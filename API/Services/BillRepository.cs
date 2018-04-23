using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public class BillRepository : GenericRepository<BillEntity, BillDto, BillForCreationDto>, IBillRepository
    {
        private DatabaseContext _context;
        private DbSet<BillEntity> _entity;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BillRepository(DatabaseContext context, UserManager<UserEntity> userManager,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _entity = _context.Set<BillEntity>();
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public new async Task<Guid> CreateAsync(BillForCreationDto creationDto)
        {

            var bill = new BillEntity();
            foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
            {
                if (bill.GetType().GetProperty(propertyInfo.Name) != null && propertyInfo.Name != "ProductList")
                {
                    bill.GetType().GetProperty(propertyInfo.Name).SetValue(bill, propertyInfo.GetValue(creationDto, null));
                }
            }
            bill.IsActive = true;
            
            //newWarehousing.CreatedDateTime = DateTime.Now;

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            bill.CreatedUserId = user.Id;
            //bill.DestroyUserId = null;
            bill.ProductList = JsonConvert.SerializeObject(creationDto.ProductList);
            double productMoney = 0;
            double productDiscountPercent = 0;
            

            foreach (var product in creationDto.ProductList)
            {
                // tao 1 select box de chon product co ban le hay khong? 
                if (bill.IsRetail == false)
                {
                    //if(product.DiscountPercent == null)
                    productDiscountPercent = (product.Amount * product.WholeSalePrice * product.DiscountPercent) / 100;
                    productMoney += (product.Amount * product.WholeSalePrice - productDiscountPercent);
                    
                }
                   
                else{
                    productDiscountPercent = (product.Amount * product.RetailPrice * product.DiscountPercent) / 100;
                    productMoney += (product.Amount * product.RetailPrice - productDiscountPercent);
                   
                }
                    
            }

            bill.TotalMoney = productMoney;

            // update inventory of ProductStorages 
            foreach (var product in creationDto.ProductList)
            {
                // update capital price with average
                var productStorage = await _context.ProductStorages.SingleOrDefaultAsync(p => p.ProductId == product.Id && p.StorageId == bill.StorageId);
                // Giảm số tồn kho
                productStorage.Inventory -= product.Amount;
                _context.ProductStorages.Update(productStorage);
            }
            await _entity.AddAsync(bill);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create Bill.");
            }

            return bill.Id;
        }

        public new async Task<Guid> DestroyAsync(Guid id)
        {
            var entity = await _entity.SingleOrDefaultAsync(r => r.Id == id);
            if (entity == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
            }

            entity.IsActive = false;
            entity.DestroyDateTime = DateTime.Now;

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            entity.DestroyUserId = user.Id;
            var products = JsonConvert.DeserializeObject<List<ProductForBillCreationDto>>(entity.ProductList);

            // update inventory of ProductStorages 
            foreach (var product in products)
            {
                // update capital price with average
                var productStorage = await _context.ProductStorages.SingleOrDefaultAsync(p => p.ProductId == product.Id && p.StorageId == entity.StorageId);
                // Tăng số tồn kho
                productStorage.Inventory += product.Amount;
                _context.ProductStorages.Update(productStorage);
            }

            _entity.Update(entity);
            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException("Database context could not update data.");
            }
            return id;
        }

        //viet ham tra ve tong tien cua tung loai san pham de khi nguoi dung thay doi amount se tu dong thay doi tong tien
    }
}

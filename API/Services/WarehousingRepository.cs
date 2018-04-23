using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Entities;
using API.Infrastructure;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Services
{
    public class WarehousingRepository : GenericRepository<WarehousingEntity, WarehousingDto, WarehousinForCreationDto>, IWarehousingRepository
    {
        private DatabaseContext _context;
        private DbSet<WarehousingEntity> _entity;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WarehousingRepository(DatabaseContext context, UserManager<UserEntity> userManager,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _entity = _context.Set<WarehousingEntity>();
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public new async Task<PagedResults<WarehousingDto>> GetAllAsync()
        {
            IQueryable<WarehousingEntity> query = _entity;
            var totalSize = await query.CountAsync();

            var warehousingEntities = await query.ToArrayAsync();

            var warehousingDtos = new List<WarehousingDto>();

            foreach( var warehousingEntity in warehousingEntities )
            {
                var warehousingDto = Mapper.Map<WarehousingDto>(warehousingEntity);
                warehousingDto.ProductList = JsonConvert.DeserializeObject<List<ProductForWareshousingCreationDto>>(warehousingEntity.ProductList);
                warehousingDtos.Add(warehousingDto);
            }

            return new PagedResults<WarehousingDto>
            {
                Items = warehousingDtos,
                TotalSize = totalSize
            };
        }

        public new async Task<Guid> CreateAsync(WarehousinForCreationDto creationDto)
        {

            var newWarehousing = new WarehousingEntity();
            foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
            {
                if (newWarehousing.GetType().GetProperty(propertyInfo.Name) != null && propertyInfo.Name != "ProductList")
                {
                    newWarehousing.GetType().GetProperty(propertyInfo.Name).SetValue(newWarehousing, propertyInfo.GetValue(creationDto, null));
                }
            }
            newWarehousing.IsActive = true;
            //newWarehousing.CreatedDateTime = DateTime.Now;

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            newWarehousing.CreatedUserId = user.Id;
            newWarehousing.ProductList = JsonConvert.SerializeObject(creationDto.ProductList);
            double productMoney = 0;

            foreach (var product in creationDto.ProductList)
            {
                // calculate product money 
                productMoney += product.InputAmount * product.InputPrice;
            }

            newWarehousing.SummaryMoney = productMoney;
            newWarehousing.PaymentMoney = creationDto.PaymentMoney;
            newWarehousing.DebtMoney = newWarehousing.SummaryMoney - creationDto.PaymentMoney;

            // update inventory of ProductStorages 
            foreach (var product in creationDto.ProductList)
            {
                // update capital price with average
                var productStorage = await _context.ProductStorages.SingleOrDefaultAsync(p => p.ProductId == product.Id && p.StorageId == newWarehousing.StorageId);
                // Tăng số tồn kho
                productStorage.Inventory += product.InputAmount;
                _context.ProductStorages.Update(productStorage);
            }
            await _entity.AddAsync(newWarehousing);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create Warehousing.");
            }

            return newWarehousing.Id;
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
            var products = JsonConvert.DeserializeObject<List<ProductForWareshousingCreationDto>>(entity.ProductList);

            // update inventory of ProductStorages 
            foreach (var product in products)
            {
                // update capital price with average
                var productStorage = await _context.ProductStorages.SingleOrDefaultAsync(p => p.ProductId == product.Id && p.StorageId == entity.StorageId);
                // Tăng số tồn kho
                productStorage.Inventory -= product.InputAmount;
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
    }
}

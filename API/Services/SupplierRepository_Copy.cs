using API.Entities;
using API.Infrastructure;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public class SupplierRepository_Copy : GenericRepository<SupplierEntity, SupplierDto, ProductDto>, ISupplierRepository_Copy
    {
        private readonly DatabaseContext _context;
        private DbSet<SupplierEntity> _entities;
        private IMapper _mapper;
        string errorMessage = string.Empty;

        public SupplierRepository_Copy(DatabaseContext context): base(context)
        {
            _context = context;
            _entities = _context.Set<SupplierEntity>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SupplierEntity, SupplierDto>();
            });

            _mapper = config.CreateMapper();
        }

        //public async Task<string[]> GeListAsync_Code()
        //{

        //    IQueryable<SupplierEntity> query = _entities;
        //    var totalSize = await query.CountAsync();
        //   string[] Code = await _context.Suppliers.Select(column => column.Code).ToArrayAsync();

        //    //var items = await query
        //    //    .ProjectTo<string>()
        //    //    .ToArrayAsync();
        //    return Code;
        //    //return new Array
        //    //{
        //    //    Items = name,
        //    //    TotalSize = totalSize
        //    //};
        //}
        public async Task<Guid> CreateAsync(SupplierDto supplierDto)
        {

            SupplierEntity newEntity = Activator.CreateInstance<SupplierEntity>();

            foreach (PropertyInfo propertyInfo in supplierDto.GetType().GetProperties()) { 
                
                if (newEntity.GetType().GetProperty(propertyInfo.Name) != null)
                {
                    newEntity.GetType().GetProperty(propertyInfo.Name).SetValue(newEntity, propertyInfo.GetValue(supplierDto, null));
                }

            }
            //trung code nha san xuat
            var entity = await _entities.SingleOrDefaultAsync(r => r.Code == supplierDto.Code);
            if (entity != null)
            {
                throw new InvalidOperationException("Mã trùng bạn cần nhập lại");
            }
            var result = await _entities.AddAsync(newEntity);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create data.");
            }
            return newEntity.Id;
        }




        public async Task<Guid> EditAsync(Guid id, SupplierDto supplierDto)
        {
            var entity = await _entities.SingleOrDefaultAsync(r => r.Id == id);
            if (entity == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
            }
            foreach (PropertyInfo propertyInfo in supplierDto.GetType().GetProperties())
            {
                string key = propertyInfo.Name;
                if (key != "Id" && entity.GetType().GetProperty(key) != null)
                {
                    entity.GetType().GetProperty(key).SetValue(entity, propertyInfo.GetValue(supplierDto, null));
                }
            }

            _entities.Update(entity);
            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException("Database context could not update data.");
            }
            return id;
        }

    }
   
       
}

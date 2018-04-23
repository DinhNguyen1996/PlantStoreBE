using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ISupplierRepository_Copy : IGenericRepository<SupplierEntity, SupplierDto, SupplierDto>
    {
        Task<PagedResults<SupplierDto>> GetListAsync(int offset, int limit, string keyword,
           SortOptions<SupplierDto, SupplierEntity> sortOptions, FilterOptions<SupplierDto, SupplierEntity> filterOptions,
            IQueryable<SupplierEntity> querySearch
            );

        new Task<Guid> CreateAsync(SupplierDto supplierDto);

        new Task<Guid> EditAsync(Guid id, SupplierDto supplierDto);

    }
}

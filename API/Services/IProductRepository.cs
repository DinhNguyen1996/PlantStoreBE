﻿using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IProductRepository : IGenericRepository<ProductEntity, ProductDto, ProductForCreationDto>
    {
        new Task<PagedResults<ProductDto>> GetListAsync(int offset, int limit, string keyword,
            SortOptions<ProductDto, ProductEntity> sortOptions,
            FilterOptions<ProductDto, ProductEntity> filterOptions,
            IQueryable<ProductEntity> querySearch
            );

        new Task<Guid> CreateAsync(ProductForCreationDto productDto);

        new Task<Guid> EditAsync(Guid id, ProductForCreationDto productDto);

    }
}

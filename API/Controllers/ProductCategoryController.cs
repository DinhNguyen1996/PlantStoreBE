using API.Entities;
using API.Infrastructure;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("ProductCategories")]
    [Authorize]
    public class ProductCategoryController : GenericController<ProductCategoryEntity, ProductCategoryDto, ProductCategoryDto>
    {

        private readonly DatabaseContext _context;
        private readonly IGenericRepository<ProductCategoryEntity, ProductCategoryDto, ProductCategoryDto> _genericRepository;
        private readonly DbSet<ProductCategoryEntity> _entity;

        public ProductCategoryController(IGenericRepository<ProductCategoryEntity, ProductCategoryDto, ProductCategoryDto> genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<ProductCategoryEntity>();

        }

        public async Task<IActionResult> GetProductCategoryAsync(
             [FromQuery] int offset,
             [FromQuery] int limit,
             [FromQuery] string keyword,
             [FromQuery] SortOptions<ProductCategoryDto, ProductCategoryEntity> sortOptions,
             [FromQuery] FilterOptions<ProductCategoryDto, ProductCategoryEntity> filterOptions)
        {
            IQueryable<ProductCategoryEntity> querySearch = _entity.Where(x => x.Name.Contains(keyword));

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, filterOptions, querySearch);

            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;

            return Ok(new { data = items, totalSize });
        }
    }
}

using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using API.Services;
using API.Infrastructure;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("Products")]
    [Authorize]
    public class ProductController : GenericController<ProductEntity, ProductDto, ProductForCreationDto>
    {
        private readonly DatabaseContext _context;
        private readonly IProductRepository _productRepository;
        private readonly DbSet<ProductEntity> _entity;

        public ProductController(IProductRepository productRepository, DatabaseContext context) : base(productRepository, context)
        {
            _productRepository = productRepository;
            _context = context;
            _entity = _context.Set<ProductEntity>();

        }

        public async Task<IActionResult> GetProductsAsync(
             [FromQuery] int offset,
             [FromQuery] int limit,
             [FromQuery] string keyword,
             [FromQuery] SortOptions<ProductDto, ProductEntity> sortOptions,
             [FromQuery] FilterOptions<ProductDto, ProductEntity> filterOptions)
        {
            IQueryable<ProductEntity> querySearch = _entity.Where(x => x.Code.Contains(keyword)
            || x.Name.Contains(keyword));

            var handledData = await _productRepository.GetListAsync(offset, limit, keyword, sortOptions, filterOptions, querySearch);

            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;

            return Ok(new { data = items, totalSize });
        }
        [HttpGet]
        [Route("CheckCodeExist")]
        public async Task<Boolean> CheckCodeExistAsync([FromQuery] string code)
        {
            var entity = await _entity.SingleOrDefaultAsync(r => r.Code == code);
            if (entity == null)
            {
                return false;
            }
            else
                return true;
        }
        [HttpGet]
        [Route("getListCodeProducts")]
        public async Task<string[]> GeListAsync_Code()
        {
            IQueryable<ProductEntity> query = _entity;
            var totalSize = await query.CountAsync();
            string[] Code = await _entity.Select(column => column.Code).ToArrayAsync();
            return Code;
        }


    }
}

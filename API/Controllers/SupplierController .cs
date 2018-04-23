using API.Entities;
using API.Infrastructure;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("Suppliers_nhap")]
    //[Authorize]
    public class SupplierController : GenericController<SupplierEntity, SupplierDto, SupplierDto>
    {
        private readonly DatabaseContext _context;
        private readonly IGenericRepository<SupplierEntity, SupplierDto, SupplierDto> _genericRepository;
        private readonly DbSet<SupplierEntity> _entity;
        private static readonly HttpClient client = new HttpClient();


        public SupplierController(IGenericRepository<SupplierEntity, SupplierDto, SupplierDto> genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<SupplierEntity>();

        }
        public async Task<IActionResult> GeListAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<SupplierDto, SupplierEntity> sortOptions,
         [FromQuery] FilterOptions<SupplierDto, SupplierEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            IQueryable<SupplierEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Name.Contains(keyword)
                || x.Code.Contains(keyword) || x.Address.Contains(keyword) || x.Phone.Contains(keyword) || x.Email.Contains(keyword)
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }

        [Route("checkCode")]
        //public async Task<IActionResult> check()
        public async Task<string[]> GeListAsync_Code()
        {

            IQueryable<SupplierEntity> query = _entity;
            var totalSize = await query.CountAsync();
            string[] Code = await _context.Suppliers.Select(column => column.Code).ToArrayAsync();

            //var items = await query
            //    .ProjectTo<string>()
            //    .ToArrayAsync();
            return Code;
            //return new Array
            //{
            //    Items = name,
            //    TotalSize = totalSize
            //};
        }

    }
}

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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("Suppliers")]
    //[Authorize]
    public class SupplierController_Copy : GenericController<SupplierEntity, SupplierDto, SupplierDto>
    {
        private readonly DatabaseContext _context;
        private readonly ISupplierRepository_Copy _supplierRepository;
        private readonly DbSet<SupplierEntity> _entity;


        public SupplierController_Copy(ISupplierRepository_Copy supplierRepository, DatabaseContext context) : base(supplierRepository, context)
        {
            _supplierRepository = supplierRepository;
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

            var handledData = await _supplierRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }
        [HttpGet]
        [Route("ListCodeSuppliers")]
        public async Task<string[]> GeListAsync_Code()
        {
            IQueryable<SupplierEntity> query = _entity;
            var totalSize = await query.CountAsync();
            string[] Code = await _entity.Select(column => column.Code).ToArrayAsync();
            return Code;
        }

    }





}

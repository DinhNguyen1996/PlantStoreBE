using API.Entities;
using API.Infrastructure;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("Customers")]
    [Authorize]

    public class CustomerController : GenericController<CustomerEntity, CustomerDto, CustomerDto>
    {
        private readonly DatabaseContext _context;
        private readonly IGenericRepository<CustomerEntity, CustomerDto, CustomerDto> _genericRepository;
        private readonly DbSet<CustomerEntity> _entity;
        private static readonly HttpClient client = new HttpClient();


        public CustomerController(IGenericRepository<CustomerEntity, CustomerDto, CustomerDto> genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<CustomerEntity>();

        }
   
        public async Task<IActionResult> GeListAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<CustomerDto,CustomerEntity> sortOptions,
         [FromQuery] FilterOptions<CustomerDto, CustomerEntity> CustomerOptions,
         [FromQuery] string keyword
       
         )
        {
            IQueryable<CustomerEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Name.Contains(keyword) || x.Code.Contains(keyword)|| x.BirthDate.Contains(keyword) ||
                     x.Address.Contains(keyword) || x.Email.Contains(keyword) || x.PhoneNumber.Contains(keyword)||
                     x.CompanyEmail.Contains(keyword) || x.CompanyAddress.Contains(keyword) || x.CompanyName.Contains(keyword) || 
                     x.CompanyPhone.Contains(keyword) || x.CompanyTaxCode.Contains(keyword) ||
                     x.ShipAddress.Contains(keyword) || x.ShipContactName.Contains(keyword) || x.ShipPhone.Contains(keyword)            
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, CustomerOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }

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
        [Route("getListCodeCustomers")]
        public async Task<string[]> GeListAsync_Code()
        {
            IQueryable<CustomerEntity> query = _entity;
            var totalSize = await query.CountAsync();
            string[] Code = await _entity.Select(column => column.Code).ToArrayAsync();
            return Code;
        }



    }
}

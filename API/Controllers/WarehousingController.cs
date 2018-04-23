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
    [Route("Warehousing")]
    [Authorize]
    public class WarehousingController : GenericController<WarehousingEntity, WarehousingDto, WarehousinForCreationDto>

    {
        private readonly DatabaseContext _context;
        private readonly IWarehousingRepository _warehousingRepository;
        private readonly DbSet<WarehousingEntity> _entity;

        public WarehousingController(IWarehousingRepository warehousingRepository, DatabaseContext context) : base(warehousingRepository, context)
        {
            _warehousingRepository = warehousingRepository;
            _context = context;
            _entity = _context.Set<WarehousingEntity>();
        }

        public async Task<IActionResult> GetWarehousingsAsync(
             [FromQuery] int offset,
             [FromQuery] int limit,
             [FromQuery] string keyword,
             [FromQuery] SortOptions<WarehousingDto, WarehousingEntity> sortOptions,
             [FromQuery] FilterOptions<WarehousingDto, WarehousingEntity> filterOptions)
        {
            IQueryable<WarehousingEntity> querySearch = _entity.Where(x => x.Code.Contains(keyword));

            var handledData = await _warehousingRepository.GetListAsync(offset, limit, keyword, sortOptions, filterOptions, querySearch);

            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;

            return Ok(new { data = items, totalSize });
        }

        [Route("allwarehousing")]
        public new async Task<IActionResult> GetAllWarehousingAsync()
        {
            var handledData = await _warehousingRepository.GetAllAsync();

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

        [Route("DestroyEntity")]
        public async virtual Task<IActionResult> DestroyEntityAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            try
            {
                var ProductId = await _warehousingRepository.DestroyAsync(id);
                return Ok(new { id = ProductId });

            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponse(ex.Message));
            }
        }
    }
}

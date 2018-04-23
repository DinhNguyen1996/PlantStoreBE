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
    [Route("storages")]
    [Authorize]
    public class StorageController : GenericController<StorageEntity, StorageDto, StorageDto>
    {
        private readonly DatabaseContext _context;
        private readonly IGenericRepository<StorageEntity, StorageDto, StorageDto> _genericRepository;
        private readonly DbSet<StorageEntity> _entity;

        public StorageController(IGenericRepository<StorageEntity, StorageDto, StorageDto> genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<StorageEntity>();

        }

        public async Task<IActionResult> GetStoragesAsync(
             [FromQuery] int offset,
             [FromQuery] int limit,
             [FromQuery] string keyword,
             [FromQuery] SortOptions<StorageDto, StorageEntity> sortOptions,
             [FromQuery] FilterOptions<StorageDto, StorageEntity> filterOptions)
        {
            IQueryable<StorageEntity> querySearch = _entity.Where(x => x.Name.Contains(keyword));

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, filterOptions, querySearch);

            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;

            return Ok(new { data = items, totalSize });
        }

        [Route("GetNameStorage")]
        public async Task<IActionResult> GetNameStorageAsync(Guid id)
        {
            try
            {
                var handledData = await _genericRepository.GetSingleAsync(id);
                return Ok(handledData.Name);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponse(ex.Message));
            }
        }
    }
}

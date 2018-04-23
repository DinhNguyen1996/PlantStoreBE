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
    [Route("Bill")]
    [Authorize]
    public class BillController : GenericController<BillEntity, BillDto, BillForCreationDto>
    {
        private readonly DatabaseContext _context;
        private readonly IBillRepository _billRepository;
        private readonly DbSet<BillEntity> _entity;
        private readonly DbSet<ProductStorageEntity> _entityProductStorage;
        //_entity = _context.Set<ProductEntity>();

        public BillController(IBillRepository billRepository, DatabaseContext context) : base(billRepository, context)
        {
            _billRepository = billRepository;
            _context = context;
            _entity = _context.Set<BillEntity>();
        }

        [Route("DestroyEntity")]
        public async virtual Task<IActionResult> DestroyEntityAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            try
            {
                var ProductId = await _billRepository.DestroyAsync(id);
                return Ok(new { id = ProductId });

            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponse(ex.Message));
            }
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

        [Route("ShowInventory")]
        public async Task<int> ShowInventoryAsync(Guid ProductId, Guid StorageId)
        {
            var entityProductStorage = await _entityProductStorage.SingleOrDefaultAsync(p => p.ProductId == ProductId && p.StorageId == StorageId);
            if (entityProductStorage == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
            }
            return entityProductStorage.Inventory;
            
        }

    }
}

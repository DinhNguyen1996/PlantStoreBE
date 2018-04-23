using API.Entities;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IBillRepository : IGenericRepository<BillEntity, BillDto, BillForCreationDto>
    {
        new Task<Guid> CreateAsync(BillForCreationDto creationDto);

        new Task<Guid> DestroyAsync(Guid id);
    }
}

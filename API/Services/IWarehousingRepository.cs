using API.Entities;
using API.Infrastructure;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IWarehousingRepository : IGenericRepository<WarehousingEntity, WarehousingDto, WarehousinForCreationDto>
    {

        new Task<PagedResults<WarehousingDto>> GetAllAsync();
        new Task<Guid> CreateAsync(WarehousinForCreationDto creationDto);
        new Task<Guid> DestroyAsync(Guid id);
    }
}

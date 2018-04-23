using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductStorageDto : BaseDto
    {
        public Guid ProductId { get; set; }

        public Guid StorageId { get; set; }

        public int Inventory { get; set; }

        public ProductEntity Product { get; set; }

        public StorageEntity Storage { get; set; }
    }
}

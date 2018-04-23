using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProductStorageEntity : BaseEntity
    {
        public Guid ProductId { get; set; }

        public Guid StorageId { get; set; }

        public int Inventory { get; set; }

        public ProductEntity Product { get; set; }

        public StorageEntity Storage { get; set; }
        

    }
}
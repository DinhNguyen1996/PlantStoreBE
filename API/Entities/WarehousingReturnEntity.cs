using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class WarehousingReturnEntity : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime ReturnDateTime { get; set; }

        [Required]
        public String ProductList { get; set; }

        public string Note { get; set; }

        [Required]
        public Guid StorageId { get; set; }
        public StorageEntity Storage { get; set; }

        [Required]
        public Guid ReturnUserId { get; set; }
        public UserEntity ReturnUser { get; set; }
    }
}

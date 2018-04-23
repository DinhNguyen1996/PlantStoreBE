using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class BillEntity : BaseEntity
    {
 
        [Required]
        public string Code { get; set; }      
        [Required]
        public bool IsRetail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public DateTime DestroyDateTime { get; set; }
        [Required]
        public String ProductList { get; set; }
        [Required]
        public double TotalMoney { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }

        [Required]
        public Guid StorageId { get; set; }
        public StorageEntity Storage { get; set; }

        public Guid CreatedUserId { get; set; }
        public Guid DestroyUserId { get; set; }
        public UserEntity CreatedUser { get; set; }
        //public UserEntity DestroyUser { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class WarehousingEntity : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public DateTime DestroyDateTime { get; set; }

        [Required]
        public double SummaryMoney { get; set; }
        [Required]
        public double PaymentMoney { get; set; }
        [Required]
        public double DebtMoney { get; set; }

        [Required]
        public String ProductList { get; set; }

        public string Note { get; set; }

        [Required]
        public Guid StorageId { get; set; }
        public StorageEntity Storage { get; set; }

        [Required]
        public Guid SupplierId { get; set; }
        public SupplierEntity Supplier { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public Guid CreatedUserId { get; set; }
        public Guid DestroyUserId { get; set; }
        public UserEntity CreatedUser { get; set; }
    }
}

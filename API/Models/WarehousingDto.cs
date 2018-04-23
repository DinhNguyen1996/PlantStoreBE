using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class WarehousingDto : BaseDto
    {
        [Required(ErrorMessage = "You should provide a Code value.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You should provide a InputDate value.")]
        public DateTime CreatedDateTime { get; set; }
        [Required(ErrorMessage = "You should provide a DestroyDate value.")]
        public DateTime DestroyDateTime { get; set; }

        [Required(ErrorMessage = "You should provide a SummaryMoney value.")]
        public double SummaryMoney { get; set; }
        [Required(ErrorMessage = "You should provide a PaymentMoney value.")]
        public double PaymentMoney { get; set; }
        [Required(ErrorMessage = "You should provide a DebtDays value.")]
        public double DebtDays { get; set; }

        public string Note { get; set; }

        [Required(ErrorMessage = "You should provide a ProductList value.")]
        public List<ProductForWareshousingCreationDto> ProductList { get; set; }

        [Required(ErrorMessage = "You should provide a StorageId value.")]
        public Guid StorageId { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "You should provide a SupplierId value.")]
        public Guid SupplierId { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid DestroyUserId { get; set; }
        
    }
}

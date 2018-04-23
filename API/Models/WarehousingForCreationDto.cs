using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class WarehousinForCreationDto : BaseDto
    {
        [Required(ErrorMessage = "You should provide a Code value.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You should provide a InputDate value.")]
        public DateTime CreatedDateTime { get; set; }

        [Required(ErrorMessage = "You should provide a ProductList value.")]
        public List<ProductForWareshousingCreationDto> ProductList { get; set; }

        
        [Required(ErrorMessage = "You should provide a PaymentMoney value.")]
        public double PaymentMoney { get; set; }
        public double DebtDays { get; set; }
        public double SummaryMoney { get; set; }

        public string Note { get; set; }

        [Required(ErrorMessage = "You should provide a StorageId value.")]
        public Guid StorageId { get; set; }

        [Required(ErrorMessage = "You should provide a SupplierId value.")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "You should provide a UserId value.")]
        public Guid CreatedUserId { get; set; }
    }
}

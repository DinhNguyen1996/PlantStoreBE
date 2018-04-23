using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class BillDto : BaseDto
    {
        [Required(ErrorMessage = "You should provide a Code value.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You should provide a InputDate value.")]
        public DateTime CreatedDateTime { get; set; }

        [Required(ErrorMessage = "You should provide a ProductList value.")]
        public List<ProductForBillCreationDto> ProductList { get; set; }

        public double TotalMoney { get; set; }

        [Required(ErrorMessage = "You should provide a CustomerId value.")]
        public Guid CustomerId { get; set; }
        public List<CustomerDto> CustomerBillList { get; set; }

        [Required(ErrorMessage = "You should provide a UserId value.")]
        public Guid UserId { get; set; }
    }
}

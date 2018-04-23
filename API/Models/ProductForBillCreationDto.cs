using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductForBillCreationDto : BaseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double RetailPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public double DiscountPercent { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductForWareshousingCreationDto : BaseDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int InputAmount { get; set; }

        public double InputPrice { get; set; }
    }
}

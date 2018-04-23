using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductCategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string ImageUrl{ get; set; }
        ICollection<ProductEntity> Products { get; set; }
    }
}

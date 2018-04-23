using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProductCategoryEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        ICollection<ProductEntity> Products { get; set; }

    }
}


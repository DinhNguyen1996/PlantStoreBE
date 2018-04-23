
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductForCreationDto
    {
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You must provide Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "You must provide ImageUrlList")]
        public string ImageUrlList { get; set; }

        [Required(ErrorMessage = "You must provide DiscountPercent")]
        public double DiscountPercent { get; set; }

        [Required(ErrorMessage = "You must provide RetailPrice")]
        public double RetailPrice { get; set; }

        [Required(ErrorMessage = "You must provide WholeSalePrice")]
        public double WholeSalePrice { get; set; }

        public Guid ProductCategoryId { get; set; }

        public ProductCategoryDto ProductCategory { get; set; }

        public Guid CurrentStorageId { get; set; }
        public ICollection<ProductStorageDto>  ProductStorage { get; set; }

    }
}

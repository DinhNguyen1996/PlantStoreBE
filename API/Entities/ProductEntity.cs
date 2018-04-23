using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        //ProductCategory
        public Guid ProductCategoryId { get; set; }

        public ProductCategoryEntity ProductCategory { get; set; }

        public double RetailPrice { get; set; }

        public double WholeSalePrice { get; set; }

        public double DiscountPercent { get; set; }

        public string ImageUrlList { get; set; }

        public DateTime CreatedDate { get; set; }
        

    }
}

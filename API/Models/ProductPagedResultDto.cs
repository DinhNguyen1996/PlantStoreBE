using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductPagedResultDto
    {
        public class PagedResults<T>
        {
            public IEnumerable<T> Items { get; set; }

            public int TotalSize { get; set; }
        }
    }
}

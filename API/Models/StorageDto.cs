using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class StorageDto : BaseDto
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}

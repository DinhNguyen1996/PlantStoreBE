using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class CustomerEntity: BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyTaxCode { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyEmail { get; set; }

        public string ShipContactName { get; set; }

        public string ShipPhone { get; set; }

        public string ShipAddress { get; set; }
    }
}

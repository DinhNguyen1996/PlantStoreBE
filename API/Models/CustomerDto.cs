using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CustomerDto:BaseDto
    {
        
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Name")]
        public string Name { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "You must provide PhoneNumber")]
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

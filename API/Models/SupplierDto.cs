using API.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class SupplierDto : BaseDto
    {
        [Sortable]
        [Filterable]
        [Required]

        public string Code { get; set; }
        [Sortable]
        [Filterable]
        [Required]
        public string Name { get; set; }
        [Sortable]
        [Filterable]
        [Required]
        public string Phone { get; set; }
        [Sortable]
        [Filterable]
        public string Email { get; set; }
        [Sortable]
        [Filterable]
        [Required]
        public string Address { get; set; }
    }
}

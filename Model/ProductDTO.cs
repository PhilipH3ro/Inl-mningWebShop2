using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DC2.UI.Model
{
    public class ProductDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Sku { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
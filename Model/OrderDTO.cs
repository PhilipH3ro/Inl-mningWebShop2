using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DC2.UI.Model
{
    public class OrderDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int IsPaid { get; set; }
        [Required]
        public int TotalPrice { get; set; }
    }
}
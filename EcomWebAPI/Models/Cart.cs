using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPI.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public int TotalItems { get; set; }

        public decimal Subtotal { get; set; }
    }
}

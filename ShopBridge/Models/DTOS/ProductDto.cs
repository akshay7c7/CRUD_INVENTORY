using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Models.DTOS
{
    public class ProductDto
    {
        
        [Required]
        [StringLength(30, MinimumLength = 10)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Range(500,10000)]
        public double Price { get; set; }
    
    }
}
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Products

{
    public class CreateProductDto
    {
    
        [Required]
        [StringLength(100, MinimumLength =2)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set;} = string.Empty;
        [Range(0.0, double.MaxValue, ErrorMessage="Price must be greater than 0")]
        public decimal Price { get; set;}
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set;}
    }
}
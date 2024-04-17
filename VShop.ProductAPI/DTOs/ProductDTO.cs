using System.ComponentModel.DataAnnotations;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [MinLength(2)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Stock is Required")]
        public long Stock { get; set; }

        public string? ImageURL { get; set; }

        public Category? Category { get; set; }
        public int? CategoryId { get; set; }
    }
}

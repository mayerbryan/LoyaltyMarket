using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class ProductRequestModel
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string Color { get; set; }

        

        public static implicit operator Product(ProductRequestModel model)
        {
            return new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Color = model.Color
            };
        }
    }
}
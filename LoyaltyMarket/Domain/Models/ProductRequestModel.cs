using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class ProductRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
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
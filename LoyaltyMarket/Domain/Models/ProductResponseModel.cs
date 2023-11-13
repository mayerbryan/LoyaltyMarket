using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class ProductResponseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Color { get; set; }


        public static implicit operator ProductResponseModel(Product product)
        {
            ArgumentException.ThrowIfNullOrEmpty(product.Id.ToString());

            var model = new ProductResponseModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Color = product.Color
            };

            return model;
        }
    }
}
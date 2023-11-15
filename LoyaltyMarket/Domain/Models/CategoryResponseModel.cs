using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class CategoryResponseModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public required string Description { get; set;}

        public static implicit operator CategoryResponseModel(Category entity)
        {
            ArgumentException.ThrowIfNullOrEmpty(entity.Id.ToString());

            var model = new CategoryResponseModel()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Description = entity.Description
            };

            return model;
        }
    }
}
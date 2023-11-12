using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class CategoryRequestModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public static implicit operator Category(CategoryRequestModel model)
        {
            var entityModel = new Category()
            {
                Name = model.Name,
                Description = model.Description
            };

            return entityModel;
        }
    }
}
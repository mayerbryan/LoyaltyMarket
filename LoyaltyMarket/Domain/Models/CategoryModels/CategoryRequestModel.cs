using Infrastructure.Entities;

namespace Domain.Models.CategoryModels
{
    public class CategoryRequestModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        

        public static implicit operator Category(CategoryRequestModel model)
        {
            var entityModel = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };

            return entityModel;
        }
    }
}
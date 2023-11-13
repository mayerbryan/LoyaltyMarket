using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class CategoryUpdateModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        

        public static implicit operator Category(CategoryUpdateModel modelToUpdate)
        {
            var entityModel = new Category()
            {
                Id = modelToUpdate.Id,
                Name = modelToUpdate.Name,
                Description = modelToUpdate.Description
            };

            return entityModel;
        }
    }
}
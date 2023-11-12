using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class CategorySummaryResponseModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public static implicit operator CategorySummaryResponseModel(Category entity)
        {
            ArgumentException.ThrowIfNullOrEmpty(entity.Id.ToString());

            var model = new CategorySummaryResponseModel()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name
            };

            return model;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Data.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        [MaxLength(100)]
        [BsonElement("Name")]
        public required string Name { get; set; }

        [BsonRequired]
        [MaxLength(150)]
        [BsonElement("Description")]
        public required string Description { get; set; }

        [BsonRequired]
        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonRequired]
        [BsonElement("Category")]
        public required string Category { get; set; }

        [BsonRequired]
        [BsonElement("Color")]
        public required string Color { get; set; }
    }
}
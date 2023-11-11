using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Presentation.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired]
        //[BsonMaxLength(100)] need to find a fix
        public string? Name { get; set; }

        [BsonElement("Description")]
        [BsonRequired]
        //[BsonMaxLength(150)] need to find a fix
        //[BsonContains("Name")] need to find a fix
        public string? Description { get; set; }

        [BsonElement("Price")]
        [BsonRequired]
        public decimal Price { get; set; }

        [BsonElement("CategoryId")]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }

        [BsonElement("Color")]
        [BsonRequired]
        public string? Color { get; set; }
    }
}

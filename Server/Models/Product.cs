using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyMarket.Models
{
    public class Product
    {
        [Key]
        public int Id { get; private set; } // Numerical ID assigned and incremented by the database

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be up to 100 characters.")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(150, ErrorMessage = "Description must be up to 150 characters.")]
        public string Description { get; private set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; private set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; private set; }

        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; private set; }

        // Constructor for creating instances
        public Product(string name, string description, decimal price, string category, string color)
        {
            // The database will assign and increment the Id
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            Color = color;
        }
    }
}
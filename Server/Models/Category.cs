using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyMarket.Models
{
    public class Category
    {
        public string Id { get; private set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; private set; }

        public Category(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
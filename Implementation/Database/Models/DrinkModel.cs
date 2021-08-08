using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class DrinkModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double? AlcoholContent { get; set; }
        public double? Price { get; set; }
        public double? AdminPrice { get; set; }
        public int? Quantity { get; set; }
        public string BrandIds { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
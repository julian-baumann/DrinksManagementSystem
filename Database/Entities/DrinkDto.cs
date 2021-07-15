using System;
using SQLite;

namespace Database.Entities
{
    public class DrinkDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public double AdminPrice { get; set; }
        public int Quantity { get; set; }
        public DrinkTypeDto Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
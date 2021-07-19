using System;
using SQLite;

namespace Database.Entities
{
    public class BoughtDrink
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DrinkId { get; set; }
        public int Quantity { get; set; }
        public double FullPrice { get; set; }
        public string DrinkName { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}
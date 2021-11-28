using System;
using Database.Models;

namespace DrinksManagementSystem.Entities
{
    public class BoughtDrink
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
        public int Quantity { get; set; }
        public double FullPrice { get; set; }
        public string DrinkName { get; set; }
        public bool Flat { get; set; }
        public DateTime DatePurchased { get; set; }
        public DateTime DatePayed { get; set; }

        public BoughtDrink() { }

        public BoughtDrink(BoughtDrinkModel model)
        {
            FromDto(model);
        }

        public void FromDto(BoughtDrinkModel model)
        {
            Id = model.Id;
            UserId = model.UserId;
            DrinkId = model.DrinkId;
            Quantity = model.Quantity;
            FullPrice = model.FullPrice;
            DrinkName = model.DrinkName;
            Flat = model.Flat;
            DatePurchased = model.DatePurchased;
            DatePayed = model.DatePayed;
        }

        public BoughtDrinkModel ToDto()
        {
            return new BoughtDrinkModel()
            {
                Id = Id,
                UserId = UserId,
                DrinkId = DrinkId,
                Quantity = Quantity,
                FullPrice = FullPrice,
                DrinkName = DrinkName,
                Flat = Flat,
                DatePurchased = DatePurchased,
                DatePayed = DatePayed
            };
        }
    }
}
using System;

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
        public DateTime DatePurchased { get; set; }


        public BoughtDrink() { }

        public BoughtDrink(Database.Entities.BoughtDrink dto)
        {
            FromDto(dto);
        }

        public void FromDto(Database.Entities.BoughtDrink dto)
        {
            Id = dto.Id;
            UserId = dto.UserId;
            DrinkId = dto.DrinkId;
            Quantity = dto.Quantity;
            FullPrice = dto.FullPrice;
            DrinkName = dto.DrinkName;
            DatePurchased = dto.DatePurchased;
        }

        public Database.Entities.BoughtDrink ToDto()
        {
            return new Database.Entities.BoughtDrink()
            {
                Id = Id,
                UserId = UserId,
                DrinkId = DrinkId,
                Quantity = Quantity,
                FullPrice = FullPrice,
                DrinkName = DrinkName,
                DatePurchased = DatePurchased
            };
        }
    }
}
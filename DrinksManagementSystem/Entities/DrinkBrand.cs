using System;
using Database.Entities;

namespace DrinksManagementSystem.Entities
{
    public class DrinkBrand
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public DrinkBrand()
        {
        }

        public DrinkBrand(Database.Entities.DrinkBrand dto)
        {
            FromDto(dto);
        }

        public void FromDto(Database.Entities.DrinkBrand dto)
        {
            Id = dto.Id;
            Name = dto.Name;
        }

        public Database.Entities.DrinkBrand ToDto()
        {
            return new Database.Entities.DrinkBrand()
            {
                Id = Id,
                Name = Name
            };
        }

        public DrinkBrand Clone()
        {
            return new DrinkBrand()
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
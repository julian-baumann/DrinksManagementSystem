using System;
using Database.Models;

namespace DrinksManagementSystem.Entities
{
    public class DrinkBrand
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public DrinkBrand()
        {
        }

        public DrinkBrand(DrinkBrandModel model)
        {
            FromDto(model);
        }

        public void FromDto(DrinkBrandModel model)
        {
            Id = model?.Id;
            Name = model?.Name;
        }

        public DrinkBrandModel ToDto()
        {
            return new DrinkBrandModel()
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
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services.DrinksDatabase
{
    public interface IDrinkDatabaseService
    {
        List<DrinkDto> GetDrinks();
        DrinkDto GetDrink(int id);
        Task<int?> CreateDrink(DrinkDto drinkDto);
        Task<bool> UpdateDrink(DrinkDto drinkDto);
        Task<bool> RemoveDrink(int id);
    }
}
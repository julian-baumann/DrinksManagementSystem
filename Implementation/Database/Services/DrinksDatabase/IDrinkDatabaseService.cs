using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Services.DrinksDatabase
{
    public interface IDrinkDatabaseService
    {
        List<DrinkModel> GetDrinks();
        DrinkModel GetDrink(int id);
        Task<int?> CreateDrink(DrinkModel drinkModel);
        Task<bool> UpdateDrink(DrinkModel drinkModel);
        Task<bool> RemoveDrink(int id);
    }
}
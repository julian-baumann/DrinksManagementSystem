using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services.DrinksDatabase
{
    public interface IDrinksDatabaseService
    {
        void Connect(string path);
        Task<List<DrinkDto>> GetDrinks();
        Task<DrinkDto> GetDrink(int id);
        Task<int> CreateDrink(DrinkDto user);
        Task<int> UpdateDrink(DrinkDto user);
        Task<int> RemoveDrink(int id);
    }
}
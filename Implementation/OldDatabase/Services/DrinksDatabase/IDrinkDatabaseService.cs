using System.Collections.Generic;
using System.Threading.Tasks;
using OldDatabase.Entities;

namespace OldDatabase.Services.DrinksDatabase
{
    public interface IDrinkDatabaseService
    {
        void Start();
        Task<List<Drink>> GetDrinks();
        Task<Drink> GetDrink(int id);
        Task<int?> CreateDrink(Drink drink);
        Task<int> UpdateDrink(Drink drink);
        Task<int> RemoveDrink(int id);
    }
}
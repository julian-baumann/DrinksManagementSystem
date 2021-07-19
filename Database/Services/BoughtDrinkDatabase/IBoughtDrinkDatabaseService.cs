using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services.BoughtDrinkDatabase
{
    public interface IBoughtDrinkDatabaseService
    {
        void Start();
        Task<BoughtDrink[]> GetAll();
        Task<BoughtDrink[]> GetAllByUser(int userId);
        Task<BoughtDrink> Get(int id);
        Task<int?> Create(BoughtDrink user);
        Task<int> Update(BoughtDrink user);
        Task<int> Remove(int id);
    }
}
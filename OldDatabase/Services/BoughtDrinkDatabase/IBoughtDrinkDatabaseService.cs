using System.Threading.Tasks;
using OldDatabase.Entities;

namespace OldDatabase.Services.BoughtDrinkDatabase
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
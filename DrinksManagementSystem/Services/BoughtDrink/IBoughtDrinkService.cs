using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.BoughtDrink
{
    public interface IBoughtDrinkService
    {
        void Start();
        Task<Entities.BoughtDrink[]> GetAll();
        Task<Entities.BoughtDrink[]> GetAllByUser(int userId);
        Task<Entities.BoughtDrink> Get(int id);
        Task<bool> Create(Entities.BoughtDrink boughtDrink);
        Task<int> Update(Entities.BoughtDrink boughtDrink);
        Task<int> Remove(int id);
    }
}
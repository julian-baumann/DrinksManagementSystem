using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.BoughtDrink
{
    public interface IBoughtDrinkService
    {
        Entities.BoughtDrink[] GetAll();
        Entities.BoughtDrink[] GetAllUnpaidDrinksByUser(int userId);
        Entities.BoughtDrink[] GetAllPaidDrinks();
        Entities.BoughtDrink[] GetAllPaidDrinksByUser(int userId);
        Entities.BoughtDrink Get(int id);
        Task<bool> Create(Entities.BoughtDrink boughtDrink);
        Task<bool> Update(Entities.BoughtDrink boughtDrink);
        Task<bool> Remove(int id);
    }
}
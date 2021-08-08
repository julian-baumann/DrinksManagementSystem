using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.Drink
{
    public interface IDrinkService
    {
        ObservableCollection<Entities.Drink> Drinks { get; set; }

        ObservableCollection<Entities.Drink> GetAll();
        Entities.Drink Get(int id);
        Entities.BoughtDrink[] ApplyFromBoughtDrinks(Entities.BoughtDrink[] drinks);
        Task<bool> Create(Entities.Drink drink);
        Task<bool> Update(Entities.Drink drink);
        Task<bool> Remove(Entities.Drink drink);
        Task<bool> Buy(Entities.User user, Entities.Drink drink, int quantity, double price);
    }
}
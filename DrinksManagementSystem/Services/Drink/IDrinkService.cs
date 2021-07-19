using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.Drink
{
    public interface IDrinkService
    {
        ObservableCollection<Entities.Drink> Drinks { get; set; }

        void Start();
        Task<ObservableCollection<Entities.Drink>> GetAll();
        Task<Entities.Drink> Get(int id);
        Task<bool> Create(Entities.Drink drink);
        Task<bool> Update(Entities.Drink drink);
        Task<int> Remove(Entities.Drink drink);
        Task<bool> Buy(Entities.User user, Entities.Drink drink, int quantity, double price);
    }
}
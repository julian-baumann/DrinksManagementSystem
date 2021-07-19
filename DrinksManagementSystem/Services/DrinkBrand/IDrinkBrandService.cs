using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.DrinkBrand
{
    public interface IDrinkBrandService
    {
        ObservableCollection<Entities.DrinkBrand> Brands { get; set; }

        void Start();
        Task<ObservableCollection<Entities.DrinkBrand>> GetAll();
        Task<Entities.DrinkBrand> Get(string id);
        Task<int> Create(Entities.DrinkBrand brand);
        Task<int> Update(Entities.DrinkBrand brand);
        Task<int> Remove(Entities.DrinkBrand brand);
    }
}
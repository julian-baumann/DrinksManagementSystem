using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.DrinkBrand
{
    public interface IDrinkBrandService
    {
        ObservableCollection<Entities.DrinkBrand> Brands { get; set; }

        ObservableCollection<Entities.DrinkBrand> GetAll();
        Entities.DrinkBrand Get(string id);
        Task<string> Create(Entities.DrinkBrand brand);
        Task<bool> Update(Entities.DrinkBrand brand);
        Task<bool> Remove(string id);
    }
}
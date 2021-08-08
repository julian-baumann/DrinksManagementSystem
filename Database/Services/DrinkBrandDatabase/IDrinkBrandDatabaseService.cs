using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Services.DrinkBrandDatabase
{
    public interface IDrinkBrandDatabaseService
    {
        List<DrinkBrandModel> GetDrinkBrands();
        DrinkBrandModel GetDrinkBrand(string id);
        Task<string> CreateDrinkBrand(DrinkBrandModel brandModel);
        Task<bool> UpdateDrinkBrand(DrinkBrandModel brandModel);
        Task<bool> RemoveDrinkBrand(string id);
    }
}
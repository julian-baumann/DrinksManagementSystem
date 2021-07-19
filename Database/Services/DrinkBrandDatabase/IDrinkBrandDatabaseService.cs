using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services.DrinkBrandDatabase
{
    public interface IDrinkBrandDatabaseService
    {
        void Start();
        Task<List<DrinkBrand>> GetDrinkBrands();
        Task<DrinkBrand> GetDrinkBrand(string id);
        Task<int> CreateDrinkBrand(DrinkBrand brand);
        Task<int> UpdateDrinkBrand(DrinkBrand brand);
        Task<int> RemoveDrinkBrand(string id);
    }
}
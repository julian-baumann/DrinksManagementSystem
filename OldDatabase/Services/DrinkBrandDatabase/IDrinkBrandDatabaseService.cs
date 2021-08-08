using System.Collections.Generic;
using System.Threading.Tasks;
using OldDatabase.Entities;

namespace OldDatabase.Services.DrinkBrandDatabase
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
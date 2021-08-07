using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services.DrinkBrandDatabase
{
    public interface IDrinkBrandDatabaseService
    {
        List<DrinkBrandDto> GetDrinkBrands();
        DrinkBrandDto GetDrinkBrand(string id);
        Task<string> CreateDrinkBrand(DrinkBrandDto brandDto);
        Task<bool> UpdateDrinkBrand(DrinkBrandDto brandDto);
        Task<bool> RemoveDrinkBrand(string id);
    }
}
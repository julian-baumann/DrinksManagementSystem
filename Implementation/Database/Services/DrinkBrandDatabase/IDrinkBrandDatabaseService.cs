using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Services.DrinkBrandDatabase
{
    public interface IDrinkBrandDatabaseService : IDatabaseService<DrinkBrandModel, string>
    {
        List<DrinkBrandModel> GetAll();
        DrinkBrandModel Get(string id);
        Task<string> Create(DrinkBrandModel brandModel);
        Task<bool> Update(DrinkBrandModel brandModel);
        Task<bool> Remove(string id);
    }
}
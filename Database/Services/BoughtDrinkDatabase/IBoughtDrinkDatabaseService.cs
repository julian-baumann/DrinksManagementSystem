using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services.BoughtDrinkDatabase
{
    public interface IBoughtDrinkDatabaseService
    {
        BoughtDrinkDto[] GetAll();
        BoughtDrinkDto[] GetAllByUser(int userId);
        BoughtDrinkDto Get(int id);
        Task<int?> Create(BoughtDrinkDto boughtDrinkDto);
        Task<bool> Update(BoughtDrinkDto boughtDrinkDto);
        Task<bool> Remove(int id);
    }
}
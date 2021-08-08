using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Services.BoughtDrinkDatabase
{
    public interface IBoughtDrinkDatabaseService
    {
        IEnumerable<BoughtDrinkModel> GetAll();
        BoughtDrinkModel[] GetAllUnpaid();
        BoughtDrinkModel[] GetAllPaid();
        BoughtDrinkModel[] GetAllByUser(int userId);
        BoughtDrinkModel[] GetAllUnpaidByUser(int userId);
        BoughtDrinkModel[] GetAllPaidByUser(int userId);
        BoughtDrinkModel Get(int id);
        Task<int?> Create(BoughtDrinkModel boughtDrinkModel);
        Task<bool> Update(BoughtDrinkModel boughtDrinkModel);
        Task<bool> Remove(int id);
    }
}
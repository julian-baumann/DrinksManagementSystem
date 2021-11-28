using System;
using System.Collections.Generic;
using Database.Models;

namespace Database.Services.BoughtDrinkDatabase
{
    public interface IBoughtDrinkDatabaseService : IDatabaseService<BoughtDrinkModel, int>
    {
        List<BoughtDrinkModel> GetAll(DateTime fromDate, DateTime toDate, bool flat);
        BoughtDrinkModel[] GetAllUnpaid();
        IEnumerable<BoughtDrinkModel> GetAllPaid();
        BoughtDrinkModel[] GetAllByUser(int userId);
        IEnumerable<BoughtDrinkModel> GetAllUnpaidByUser(int userId);
        IEnumerable<BoughtDrinkModel> GetAllPaidByUser(int userId);
        DateTime? GetLatestChange(int id);
    }
}
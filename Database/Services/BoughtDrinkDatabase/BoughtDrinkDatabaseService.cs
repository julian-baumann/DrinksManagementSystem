using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Models;

namespace Database.Services.BoughtDrinkDatabase
{
    public class BoughtDrinkDatabaseService : IBoughtDrinkDatabaseService
    {
        public IEnumerable<BoughtDrinkModel> GetAll()
        {
            try
            {
                using var database = new DatabaseContext();
                return database.BoughtDrinks.ToArray();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public BoughtDrinkModel[] GetAllUnpaid()
        {
            try
            {
                using var database = new DatabaseContext();
                return database.BoughtDrinks.Where(entry => entry.DatePayed == new DateTime()).ToArray();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public BoughtDrinkModel[] GetAllPaid()
        {
            try
            {
                using var database = new DatabaseContext();
                return database.BoughtDrinks.Where(entry => entry.DatePayed != new DateTime()).ToArray();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }


        public BoughtDrinkModel[] GetAllByUser(int userId)
        {
            try
            {
                using var database = new DatabaseContext();
                return database.BoughtDrinks
                    .Where(i => i.UserId == userId)
                    .ToArray();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }


        public BoughtDrinkModel[] GetAllUnpaidByUser(int userId)
        {
            try
            {
                using var database = new DatabaseContext();
                return database.BoughtDrinks
                    .Where(i => i.UserId == userId)
                    .Where(entry => entry.DatePayed == new DateTime())
                    .ToArray();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }


        public BoughtDrinkModel[] GetAllPaidByUser(int userId)
        {
            try
            {
                using var database = new DatabaseContext();
                return database.BoughtDrinks
                    .Where(i => i.UserId == userId)
                    .Where(entry => entry.DatePayed != new DateTime())
                    .ToArray();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public BoughtDrinkModel Get(int id)
        {
            try
            {
                using var database = new DatabaseContext();
                return database.BoughtDrinks
                    .Where(i => i.Id == id)
                    .OrderByDescending(x => x.DatePurchased)
                    .FirstOrDefault();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }


        public async Task<int?> Create(BoughtDrinkModel boughtDrinkModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.BoughtDrinks.Add(boughtDrinkModel);
                await database.SaveChangesAsync();

                return result?.Entity?.Id;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<bool> Update(BoughtDrinkModel boughtDrinkModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.BoughtDrinks.Update(boughtDrinkModel);
                var result = await database.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.BoughtDrinks.Remove(Get(id));
                var result = await database.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return false;
            }
        }
    }
}
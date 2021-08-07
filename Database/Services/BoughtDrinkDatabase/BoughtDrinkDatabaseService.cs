using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Entities;
using Database.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace Database.Services.BoughtDrinkDatabase
{
    public class BoughtDrinkDatabaseService : IBoughtDrinkDatabaseService
    {
        public BoughtDrinkDto[] GetAll()
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

        public BoughtDrinkDto[] GetAllByUser(int userId)
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

        public BoughtDrinkDto Get(int id)
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


        public async Task<int?> Create(BoughtDrinkDto boughtDrinkDto)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.BoughtDrinks.Add(boughtDrinkDto);
                await database.SaveChangesAsync();

                return result?.Entity?.Id;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<bool> Update(BoughtDrinkDto boughtDrinkDto)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.BoughtDrinks.Update(boughtDrinkDto);
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
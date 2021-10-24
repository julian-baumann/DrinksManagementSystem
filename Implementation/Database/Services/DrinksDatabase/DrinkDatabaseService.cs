using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Models;

namespace Database.Services.DrinksDatabase
{
    public class DrinkDatabaseService : IDrinkDatabaseService
    {
        public List<DrinkModel> GetAll()
        {
            try
            {
                using var database = new DatabaseContext();
                return database.Drinks.ToList();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public DrinkModel Get(int id)
        {
            try
            {
                using var database = new DatabaseContext();
                return database.Drinks.Single(entry => entry.Id == id);
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<int> Create(DrinkModel drinkModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.Drinks.Add(drinkModel);
                await database.SaveChangesAsync();

                if (result?.Entity?.Id != null)
                {
                    return result.Entity.Id;
                }

                return -1;

            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return -1;
            }
        }

        public async Task<bool> Update(DrinkModel drinkModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Drinks.Update(drinkModel);
                var result = await database.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Drinks.Update(Get(id));
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
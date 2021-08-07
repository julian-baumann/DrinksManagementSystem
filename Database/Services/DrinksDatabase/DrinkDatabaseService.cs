using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Entities;
using Database.Services.Database;

namespace Database.Services.DrinksDatabase
{
    public class DrinkDatabaseService : IDrinkDatabaseService
    {
        public List<DrinkDto> GetDrinks()
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

        public DrinkDto GetDrink(int id)
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

        public async Task<int?> CreateDrink(DrinkDto drinkDto)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.Drinks.Add(drinkDto);
                await database.SaveChangesAsync();

                return result?.Entity?.Id;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<bool> UpdateDrink(DrinkDto drinkDto)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Drinks.Update(drinkDto);
                var result = await database.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveDrink(int id)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Drinks.Update(GetDrink(id));
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
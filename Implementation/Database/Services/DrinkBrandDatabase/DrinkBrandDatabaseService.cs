using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Models;

namespace Database.Services.DrinkBrandDatabase
{
    public class DrinkBrandDatabaseService : IDrinkBrandDatabaseService
    {
        public List<DrinkBrandModel> GetDrinkBrands()
        {
            try
            {
                using var database = new DatabaseContext();
                return database.Brands.ToList();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public DrinkBrandModel GetDrinkBrand(string id)
        {
            try
            {
                using var database = new DatabaseContext();
                return database.Brands
                    .FirstOrDefault(entry => entry.Id == id);
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<string> CreateDrinkBrand(DrinkBrandModel brandModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.Brands.Add(brandModel);
                await database.SaveChangesAsync();

                return result?.Entity?.Id;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<bool> UpdateDrinkBrand(DrinkBrandModel brandModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Brands.Update(brandModel);
                var result = await database.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return false;
            }
        }

        public async Task<bool> RemoveDrinkBrand(string id)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Brands.Remove(GetDrinkBrand(id));
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
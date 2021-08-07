using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Entities;

namespace Database.Services.DrinkBrandDatabase
{
    public class DrinkBrandDatabaseService : IDrinkBrandDatabaseService
    {
        public List<DrinkBrandDto> GetDrinkBrands()
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

        public DrinkBrandDto GetDrinkBrand(string id)
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

        public async Task<string> CreateDrinkBrand(DrinkBrandDto brandDto)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.Brands.Add(brandDto);
                await database.SaveChangesAsync();

                return result?.Entity?.Id;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<bool> UpdateDrinkBrand(DrinkBrandDto brandDto)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Brands.Update(brandDto);
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
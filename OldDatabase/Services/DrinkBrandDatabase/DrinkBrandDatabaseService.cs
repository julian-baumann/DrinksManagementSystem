using System.Collections.Generic;
using System.Threading.Tasks;
using OldDatabase.Entities;
using OldDatabase.Services.Database;
using SQLite;

namespace OldDatabase.Services.DrinkBrandDatabase
{
    public class DrinkBrandDatabaseService : IDrinkBrandDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DrinkBrandDatabaseService(IDatabaseService databaseService)
        {
            _database = databaseService.Database;
        }

        public void Start()
        {
            _database.CreateTableAsync<DrinkBrand>();
        }

        public Task<List<DrinkBrand>> GetDrinkBrands()
        {
            return _database.Table<DrinkBrand>().ToListAsync();
        }

        public Task<DrinkBrand> GetDrinkBrand(string id)
        {
            return _database.Table<DrinkBrand>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> CreateDrinkBrand(DrinkBrand brand)
        {
            return _database.InsertAsync(brand);
        }

        public Task<int> UpdateDrinkBrand(DrinkBrand brand)
        {
            return _database.UpdateAsync(brand);
        }

        public Task<int> RemoveDrinkBrand(string id)
        {
            return _database.Table<DrinkBrand>()
                .Where(i => i.Id == id)
                .DeleteAsync();
        }
    }
}
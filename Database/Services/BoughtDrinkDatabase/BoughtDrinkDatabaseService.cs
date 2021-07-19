using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;
using Database.Services.Database;
using SQLite;

namespace Database.Services.BoughtDrinkDatabase
{
    public class BoughtDrinkDatabaseService : IBoughtDrinkDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public BoughtDrinkDatabaseService(IDatabaseService databaseService)
        {
            _database = databaseService.Database;
        }

        public void Start()
        {
            _database.CreateTableAsync<BoughtDrink>();
        }

        public Task<BoughtDrink[]> GetAll()
        {
            return _database.Table<BoughtDrink>().ToArrayAsync();
        }

        public Task<BoughtDrink[]> GetAllByUser(int userId)
        {
            return _database.Table<BoughtDrink>()
                .Where(i => i.UserId == userId)
                .ToArrayAsync();
        }

        public Task<BoughtDrink> Get(int id)
        {
            return _database.Table<BoughtDrink>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<int?> Create(BoughtDrink user)
        {
            var result = await _database.InsertAsync(user);
            if (result >= 0)
            {
                return user.Id;
            }

            return null;
        }

        public Task<int> Update(BoughtDrink user)
        {
            return _database.UpdateAsync(user);
        }

        public Task<int> Remove(int id)
        {
            return _database.Table<BoughtDrink>()
                .Where(i => i.Id == id)
                .DeleteAsync();
        }
    }
}
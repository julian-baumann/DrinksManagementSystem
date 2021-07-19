using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;
using Database.Services.Database;
using SQLite;

namespace Database.Services.DrinksDatabase
{
    public class DrinkDatabaseService : IDrinkDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DrinkDatabaseService(IDatabaseService databaseService)
        {
            _database = databaseService.Database;
        }

        public void Start()
        {
            _database.CreateTableAsync<Drink>();
        }

        public Task<List<Drink>> GetDrinks()
        {
            return _database.Table<Drink>().ToListAsync();
        }

        public Task<Drink> GetDrink(int id)
        {
            return _database.Table<Drink>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int?> CreateDrink(Drink drink)
        {

            var result = await _database.InsertAsync(drink);
            if (result >= 0)
            {
                return drink.Id;
            }

            return null;
        }

        public Task<int> UpdateDrink(Drink drink)
        {
            return _database.UpdateAsync(drink);
        }

        public Task<int> RemoveDrink(int id)
        {
            return _database.Table<Drink>()
                .Where(i => i.Id == id)
                .DeleteAsync();
        }
    }
}
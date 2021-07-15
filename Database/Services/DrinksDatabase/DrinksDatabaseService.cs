using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;
using SQLite;

namespace Database.Services.DrinksDatabase
{
    public class DrinksDatabaseService : IDrinksDatabaseService
    {
        private SQLiteAsyncConnection _database;

        public void Connect(string path)
        {
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<DrinkDto>();
        }

        public Task<List<DrinkDto>> GetDrinks()
        {
            return _database.Table<DrinkDto>().ToListAsync();
        }

        public Task<DrinkDto> GetDrink(int id)
        {
            return _database.Table<DrinkDto>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> CreateDrink(DrinkDto user)
        {
            return _database.InsertAsync(user);
        }

        public Task<int> UpdateDrink(DrinkDto user)
        {
            return _database.UpdateAsync(user);
        }

        public Task<int> RemoveDrink(int id)
        {
            return _database.Table<DrinkDto>()
                .Where(i => i.Id == id)
                .DeleteAsync();
        }
    }
}
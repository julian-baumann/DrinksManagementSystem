using System.Collections.Generic;
using System.Threading.Tasks;
using OldDatabase.Entities;
using OldDatabase.Services.Database;
using SQLite;

namespace OldDatabase.Services.UserDatabase
{
    public class UserDatabaseService : IUserDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public UserDatabaseService(IDatabaseService databaseService)
        {
            _database = databaseService.Database;
        }

        public void Start()
        {
            _database.CreateTableAsync<User>();
        }

        public Task<List<User>> GetUsers()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetUser(int id)
        {
            return _database.Table<User>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<User> GetUserId(int row)
        {
            return _database.Table<User>().ElementAtAsync(row);
        }

        public async Task<int?> CreateUser(User user)
        {
            var result = await _database.InsertAsync(user);
            if (result >= 0)
            {
                return user.Id;
            }

            return null;
        }

        public Task<int> UpdateUser(User user)
        {
            return _database.UpdateAsync(user);
        }

        public Task<int> RemoveUser(int id)
        {
            return _database.Table<User>()
                .Where(i => i.Id == id)
                .DeleteAsync();
        }
    }
}
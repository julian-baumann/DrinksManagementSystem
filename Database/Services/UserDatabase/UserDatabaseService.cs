using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Core;
using Database.Entities;
using SQLite;

namespace Database.Services
{
    public class UserDatabaseService : IUserDatabaseService
    {
        private SQLiteAsyncConnection _database;

        public void Connect(string path)
        {
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<UserDto>();
        }

        public Task<List<UserDto>> GetUsers()
        {
            return _database.Table<UserDto>().ToListAsync();
        }

        public Task<UserDto> GetUser(int id)
        {
            return _database.Table<UserDto>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> CreateUser(UserDto user)
        {
            return _database.InsertAsync(user);
        }

        public Task<int> UpdateUser(UserDto user)
        {
            return _database.UpdateAsync(user);
        }

        public Task<int> RemoveUser(int id)
        {
            return _database.Table<UserDto>()
                .Where(i => i.Id == id)
                .DeleteAsync();
        }
    }
}
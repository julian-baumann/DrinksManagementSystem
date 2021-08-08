using System.Collections.Generic;
using System.Threading.Tasks;
using OldDatabase.Entities;

namespace OldDatabase.Services.UserDatabase
{
    public interface IUserDatabaseService
    {
        void Start();
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<int?> CreateUser(User user);
        Task<int> UpdateUser(User user);
        Task<int> RemoveUser(int id);
    }
}
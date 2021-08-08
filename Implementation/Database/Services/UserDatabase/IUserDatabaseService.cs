using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Services.UserDatabase
{
    public interface IUserDatabaseService
    {
        List<UserModel> GetUsers();
        UserModel GetUser(int id);
        Task<int?> CreateUser(UserModel userModel);
        Task<bool> UpdateUser(UserModel userModel);
        Task<bool> RemoveUser(int id);
    }
}
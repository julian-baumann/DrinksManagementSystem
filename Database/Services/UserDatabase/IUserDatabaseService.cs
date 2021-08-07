using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services.UserDatabase
{
    public interface IUserDatabaseService
    {
        List<UserDto> GetUsers();
        UserDto GetUser(int id);
        Task<int?> CreateUser(UserDto userDto);
        Task<bool> UpdateUser(UserDto userDto);
        Task<bool> RemoveUser(int id);
    }
}
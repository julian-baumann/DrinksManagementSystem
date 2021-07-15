using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Services
{
    public interface IUserDatabaseService
    {
        void Connect(string path);
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<int> CreateUser(UserDto user);
        Task<int> UpdateUser(UserDto user);
        Task<int> RemoveUser(int id);
    }
}
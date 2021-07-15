using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.User
{
    public interface IUserService
    {
        ObservableCollection<Entities.User> Users { get; set; }

        void Connect(string path);
        Task<ObservableCollection<Entities.User>> GetUsers();
        Task<Entities.User> GetUser(int id);
        Task<int> CreateUser(Entities.User user);
        Task<int> UpdateUser(Entities.User user);
        Task<int> RemoveUser(Entities.User userId);
    }
}
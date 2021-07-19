using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.User
{
    public interface IUserService
    {
        ObservableCollection<Entities.User> Users { get; set; }

        void Start();
        Task<ObservableCollection<Entities.User>> GetAll();
        Task<Entities.User> Get(int id);
        Task<bool> Create(Entities.User user);
        Task<int> Update(Entities.User user);
        Task<int> Remove(Entities.User user);
    }
}
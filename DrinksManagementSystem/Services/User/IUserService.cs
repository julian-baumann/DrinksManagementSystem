using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.User
{
    public interface IUserService
    {
        ObservableCollection<Entities.User> Users { get; set; }

        ObservableCollection<Entities.User> GetAll();
        Entities.User Get(int id);
        Task<bool> Create(Entities.User user);
        Task<bool> Update(Entities.User user);
        Task<bool> Remove(Entities.User user);
    }
}
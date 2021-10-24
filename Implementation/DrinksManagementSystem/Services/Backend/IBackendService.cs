using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.Backend
{
    public interface IBackendService
    {
        void Initialize(string serverUrl);
        Task UploadUsers();
        Task UploadBoughtDrinks();
    }
}
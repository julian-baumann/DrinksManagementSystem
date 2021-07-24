using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.Share
{
    public interface IShareService
    {
        Task ShareDatabase();
        Task<bool?> ImportDatabase();
    }
}
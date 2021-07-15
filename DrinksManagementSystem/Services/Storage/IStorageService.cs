using System;
using System.IO;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.Storage
{
    public interface IStorageService
    {
        Task<string> StoreProfilePicture(string name, Stream fileSream);
        Task RemoveProfilePicture(string filePath);
    }
}

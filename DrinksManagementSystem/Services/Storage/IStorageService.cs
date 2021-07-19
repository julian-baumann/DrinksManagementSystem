using System;
using System.IO;
using System.Threading.Tasks;

namespace DrinksManagementSystem.Services.Storage
{
    public interface IStorageService
    {
        Task<string> StorePicture(string name, Stream fileSream);
        Task RemovePicture(string filePath);
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Core;
using PCLStorage;

namespace DrinksManagementSystem.Services.Storage
{
    public class StorageService : IStorageService
    {
        public async Task<string> StorePicture(string name, Stream fileStream)
        {
            var fileName = Path.GetFileName(name);
            var rootFolder = await FileSystem.Current.GetFolderFromPathAsync(AppCore.StoragePath);
            var folder = await rootFolder.CreateFolderAsync("Pictures", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

            using var targetStream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite);
            await fileStream.CopyToAsync(targetStream);

            var fullPath = $"Pictures/{file.Name}";

            return fullPath;
        }

        public async Task RemovePicture(string filePath)
        {
            try
            {
                var fileName = Path.GetFileName(filePath);
                var rootFolder = await FileSystem.Current.GetFolderFromPathAsync(AppCore.StoragePath);
                var folder = await rootFolder.CreateFolderAsync("Pictures", CreationCollisionOption.OpenIfExists);
                var file = await folder.GetFileAsync(fileName);

                if (file != null)
                {
                    await file.DeleteAsync();
                }
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }
        }
    }
}

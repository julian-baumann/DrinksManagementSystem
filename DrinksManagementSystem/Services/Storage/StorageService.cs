using System;
using System.IO;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Core;
using DrinksManagementSystem.Services.ImageResizer;
using PCLStorage;
using Xamarin.Forms;

namespace DrinksManagementSystem.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IImageResizerService _imageResizerService;

        public StorageService()
        {
            _imageResizerService = DependencyService.Get<IImageResizerService>();
            // _imageResizerService = imageResizerService;
        }

        public static byte[] ConvertStreamToByteArray(Stream input)
        {
            using var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }

        public async Task<string> StorePicture(string name, Stream fileStream)
        {
            var originalFileBytes = ConvertStreamToByteArray(fileStream);
            var resizedImage = _imageResizerService.ResizeImage(originalFileBytes, 300, 300);
            var fileName = Path.GetFileName(name);
            var rootFolder = await FileSystem.Current.GetFolderFromPathAsync(AppCore.StoragePath);
            var folder = await rootFolder.CreateFolderAsync("Pictures", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

            using var targetStream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite);
            var stream = new MemoryStream(resizedImage);
            await stream.CopyToAsync(targetStream);

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

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Core;
using Xamarin.Essentials;

namespace DrinksManagementSystem.Services.Share
{
    public class ShareService : IShareService
    {
        private bool QuickZip(string destinationZipFullPath, string databasePath, IEnumerable<string> picturePaths)
        {
            try
            {
                if (File.Exists(destinationZipFullPath))
                    File.Delete(destinationZipFullPath);

                using var zip = ZipFile.Open(destinationZipFullPath, ZipArchiveMode.Create);

                zip.CreateEntryFromFile(databasePath, Path.GetFileName(databasePath), CompressionLevel.Optimal);

                foreach (var photoPath in picturePaths)
                {
                    zip.CreateEntryFromFile(photoPath, $"Pictures/{Path.GetFileName(photoPath)}", CompressionLevel.Optimal);
                }

                return File.Exists(destinationZipFullPath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return false;
            }
        }

        private void Extract(string zipPath)
        {
            var databasePath = Path.Combine(AppCore.StoragePath, "DMSDB.db3");
            var photosPath = Path.Combine(AppCore.StoragePath, "Pictures");

            if (Directory.Exists(photosPath))
            {
                Directory.Delete(photosPath, true);
            }

            Directory.CreateDirectory(photosPath);

            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }

            using var zip = ZipFile.Open(zipPath, ZipArchiveMode.Read);

            var entries = zip.Entries;

            foreach (var entry in entries)
            {
                var path = Path.Combine(AppCore.StoragePath, entry.FullName);
                entry.ExtractToFile(path);
                Logger.Info(path);
            }
        }

        public async Task ShareDatabase()
        {
            var zipPath = Path.Combine(FileSystem.CacheDirectory, "Database.zip");
            var databasePath = Path.Combine(AppCore.StoragePath, "DMSDB.db3");
            var photosPath = Path.Combine(AppCore.StoragePath, "Pictures");

            var directoryInfo = new DirectoryInfo(photosPath);
            var files = directoryInfo.GetFiles("*").Select(file => $"{photosPath}/{file.Name}");
            var zipSuccessful = QuickZip(zipPath, databasePath, files);

            if (zipSuccessful)
            {
                await Xamarin.Essentials.Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Share Database",
                    File = new ShareFile(zipPath)
                });
            }
        }

        public async Task<bool?> ImportDatabase()
        {
            try
            {
                var result = await FilePicker.PickAsync();

                if (result == null) return null;

                Extract(result.FullPath);

                return true;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return false;
            }
        }
    }
}
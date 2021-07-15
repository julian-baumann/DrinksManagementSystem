using System;
using System.IO;
using System.Threading.Tasks;
using Common.Core;
using Database.Services;
using DrinksManagementSystem.Services.Storage;
using DrinksManagementSystem.Services.User;
using PCLStorage;

namespace DrinksManagementSystem.Core
{
    public class AppCore
    {
        public static string StoragePath { get; set; }

        public static async Task Initialize()
        {
            Logger.Info("Registering Services...");
            Ioc.Register<IUserDatabaseService, UserDatabaseService>();
            Ioc.Register<IUserService, UserService>();
            Ioc.Register<IStorageService, StorageService>();

            Logger.Info("Services Registered");


            StoragePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Logger.Info("Connecting to Database...");
            var userService = Ioc.Resolve<IUserService>();
            var databaseLocation = Path.Combine(StoragePath, "DMSDB.db3");
            userService.Connect(databaseLocation);
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using Common.Core;
using Database;
using Database.Services;
using Database.Services.BoughtDrinkDatabase;
using Database.Services.Database;
using Database.Services.DrinkBrandDatabase;
using Database.Services.DrinksDatabase;
using Database.Services.UserDatabase;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.Drink;
using DrinksManagementSystem.Services.DrinkBrand;
using DrinksManagementSystem.Services.Share;
using DrinksManagementSystem.Services.Storage;
using DrinksManagementSystem.Services.User;

namespace DrinksManagementSystem.Core
{
    public class AppCore
    {
        public static string StoragePath { get; set; }

        public static void Initialize()
        {
            Logger.Info("Registering Services...");

            Ioc.Register<IUserDatabaseService, UserDatabaseService>();
            Ioc.Register<IDrinkDatabaseService, DrinkDatabaseService>();
            Ioc.Register<IDrinkBrandDatabaseService, DrinkBrandDatabaseService>();
            Ioc.Register<IBoughtDrinkDatabaseService, BoughtDrinkDatabaseService>();
            Ioc.Register<IStorageService, StorageService>();
            Ioc.Register<IUserService, UserService>();
            Ioc.Register<IDrinkService, DrinkService>();
            Ioc.Register<IDrinkBrandService, DrinkBrandService>();
            Ioc.Register<IBoughtDrinkService, BoughtDrinkService>();
            Ioc.Register<IShareService, ShareService>();

            Logger.Info("Services Registered");


            StoragePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            Logger.Info("Connecting to Database...");

            var databaseLocation = Path.Combine(StoragePath, "DMSDB.db3");

            DatabaseContext.SetDatabasePath(databaseLocation);

            Logger.Success("Finished Setup");

        }
    }
}
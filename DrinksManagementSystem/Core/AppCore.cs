using System;
using System.IO;
using System.Threading.Tasks;
using Common.Core;
using Database.Services;
using Database.Services.BoughtDrinkDatabase;
using Database.Services.Database;
using Database.Services.DrinkBrandDatabase;
using Database.Services.DrinksDatabase;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.Drink;
using DrinksManagementSystem.Services.DrinkBrand;
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

            Ioc.Register<IDatabaseService, DatabaseService>();
            Ioc.Register<IUserDatabaseService, UserDatabaseService>();
            Ioc.Register<IDrinkDatabaseService, DrinkDatabaseService>();
            Ioc.Register<IDrinkBrandDatabaseService, DrinkBrandDatabaseService>();
            Ioc.Register<IBoughtDrinkDatabaseService, BoughtDrinkDatabaseService>();
            Ioc.Register<IStorageService, StorageService>();
            Ioc.Register<IUserService, UserService>();
            Ioc.Register<IDrinkService, DrinkService>();
            Ioc.Register<IDrinkBrandService, DrinkBrandService>();
            Ioc.Register<IBoughtDrinkService, BoughtDrinkService>();

            Logger.Info("Services Registered");


            StoragePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            Logger.Info("Connecting to Database...");

            var databaseLocation = Path.Combine(StoragePath, "DMSDB.db3");

            Ioc.Resolve<IDatabaseService>().Connect(databaseLocation);
            Ioc.Resolve<IUserService>().Start();
            Ioc.Resolve<IDrinkService>().Start();
            Ioc.Resolve<IDrinkBrandService>().Start();
            Ioc.Resolve<IBoughtDrinkService>().Start();


            Logger.Success("Finished Setup");

        }
    }
}
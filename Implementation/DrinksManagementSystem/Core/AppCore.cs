using System;
using System.IO;
using Common.Core;
using Database;
using Database.Services.BoughtDrinkDatabase;
using Database.Services.DrinkBrandDatabase;
using Database.Services.DrinksDatabase;
using Database.Services.UserDatabase;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.Drink;
using DrinksManagementSystem.Services.DrinkBrand;
using DrinksManagementSystem.Services.Share;
using DrinksManagementSystem.Services.Storage;
using DrinksManagementSystem.Services.User;
using Xamarin.Forms;

namespace DrinksManagementSystem.Core
{
    public class AppCore
    {
        public static string StoragePath { get; set; }

        public static bool Flat
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("Flat"))
                {
                    return (bool)Application.Current.Properties["Flat"];
                }

                return false;
            }
            set => Application.Current.Properties["Flat"] = value;
        }

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


            Logger.Info("Connecting to Database...");

            StoragePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databaseLocation = Path.Combine(StoragePath, "DMS.db3");
            DatabaseContext.SetDatabasePath(databaseLocation);



            // Flat = Application.Current.Properties["Flat"] as bool? ?? false;

            Logger.Success("Finished Setup");
        }
    }
}
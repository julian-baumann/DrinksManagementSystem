

using System;
using System.IO;
using Common.Core;
using Database;

namespace ConsoleApp
{
    public class AppCore
    {
        public static void Initialize()
        {
            Logger.Info("Registering Old Database Services");
            Ioc.Register<OldDatabase.Services.Database.IDatabaseService, OldDatabase.Services.Database.DatabaseService>();
            Ioc.Register<OldDatabase.Services.UserDatabase.IUserDatabaseService, OldDatabase.Services.UserDatabase.UserDatabaseService>();
            Ioc.Register<OldDatabase.Services.DrinksDatabase.IDrinkDatabaseService, OldDatabase.Services.DrinksDatabase.DrinkDatabaseService>();
            Ioc.Register<OldDatabase.Services.DrinkBrandDatabase.IDrinkBrandDatabaseService, OldDatabase.Services.DrinkBrandDatabase.DrinkBrandDatabaseService>();
            Ioc.Register<OldDatabase.Services.BoughtDrinkDatabase.IBoughtDrinkDatabaseService, OldDatabase.Services.BoughtDrinkDatabase.BoughtDrinkDatabaseService>();
            Logger.Success("Old Database Services registered");


            Logger.Info("Registering New Database Services");
            Ioc.Register<Database.Services.UserDatabase.IUserDatabaseService, Database.Services.UserDatabase.UserDatabaseService>();
            Ioc.Register<Database.Services.DrinksDatabase.IDrinkDatabaseService, Database.Services.DrinksDatabase.DrinkDatabaseService>();
            Ioc.Register<Database.Services.DrinkBrandDatabase.IDrinkBrandDatabaseService, Database.Services.DrinkBrandDatabase.DrinkBrandDatabaseService>();
            Ioc.Register<Database.Services.BoughtDrinkDatabase.IBoughtDrinkDatabaseService, Database.Services.BoughtDrinkDatabase.BoughtDrinkDatabaseService>();
            Logger.Success("New Database Services registered");


            var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            Logger.Info("Connecting to OldDatabase...");
            var oldDatabaseLocation = Path.Combine(storagePath, "DMSDB-OLD.db3");
            Ioc.Resolve<OldDatabase.Services.Database.IDatabaseService>().Connect(oldDatabaseLocation);
            Ioc.Resolve<OldDatabase.Services.UserDatabase.IUserDatabaseService>().Start();
            Ioc.Resolve<OldDatabase.Services.DrinksDatabase.IDrinkDatabaseService>().Start();
            Ioc.Resolve<OldDatabase.Services.DrinkBrandDatabase.IDrinkBrandDatabaseService>().Start();
            Ioc.Resolve<OldDatabase.Services.BoughtDrinkDatabase.IBoughtDrinkDatabaseService>().Start();

            Logger.Info("Connecting to Database...");
            var databaseLocation = Path.Combine(storagePath, "DMSDB.db3");
            DatabaseContext.SetDatabasePath(databaseLocation);

            Logger.Success("Finished Setup");
        }
    }
}
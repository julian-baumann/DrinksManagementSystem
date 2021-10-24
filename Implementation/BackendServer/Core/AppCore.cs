using Common.Core;
using Database.Services.BoughtDrinkDatabase;
using Database.Services.DrinkBrandDatabase;
using Database.Services.DrinksDatabase;
using Database.Services.UserDatabase;

namespace BackendServer.Core
{
    public class AppCore
    {
        public static void Initialize()
        {
            Logger.Info("Registering Services...");

            Ioc.Register<IUserDatabaseService, UserDatabaseService>();
            Ioc.Register<IDrinkDatabaseService, DrinkDatabaseService>();
            Ioc.Register<IDrinkBrandDatabaseService, DrinkBrandDatabaseService>();
            Ioc.Register<IBoughtDrinkDatabaseService, BoughtDrinkDatabaseService>();

            Logger.Info("Services Registered");

            Logger.Success("Finished Setup");
        }
    }
}
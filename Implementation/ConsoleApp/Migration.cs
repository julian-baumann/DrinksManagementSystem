using System.Threading.Tasks;
using Common.Core;
using Database.Models;
using OldDatabase.Services.BoughtDrinkDatabase;
using OldDatabase.Services.DrinkBrandDatabase;
using OldDatabase.Services.DrinksDatabase;
using OldDatabase.Services.UserDatabase;

namespace ConsoleApp
{
    public class Migration
    {

        private readonly IUserDatabaseService _oldUserDatabase;
        private readonly IDrinkDatabaseService _oldDrinkDatabase;
        private readonly IBoughtDrinkDatabaseService _oldBoughtDrinksDatabase;
        private readonly IDrinkBrandDatabaseService _oldDrinkBrandDatabase;

        private readonly Database.Services.UserDatabase.IUserDatabaseService _userDatabase;
        private readonly Database.Services.DrinksDatabase.IDrinkDatabaseService _drinkDatabase;
        private readonly Database.Services.BoughtDrinkDatabase.IBoughtDrinkDatabaseService _boughtDrinksDatabase;
        private readonly Database.Services.DrinkBrandDatabase.IDrinkBrandDatabaseService _drinkBrandDatabase;

        public Migration()
        {
            _oldUserDatabase = Ioc.Resolve<IUserDatabaseService>();
            _oldDrinkDatabase = Ioc.Resolve<IDrinkDatabaseService>();
            _oldBoughtDrinksDatabase = Ioc.Resolve<IBoughtDrinkDatabaseService>();
            _oldDrinkBrandDatabase = Ioc.Resolve<IDrinkBrandDatabaseService>();

            _userDatabase = Ioc.Resolve<Database.Services.UserDatabase.IUserDatabaseService>();
            _drinkDatabase = Ioc.Resolve<Database.Services.DrinksDatabase.IDrinkDatabaseService>();
            _boughtDrinksDatabase = Ioc.Resolve<Database.Services.BoughtDrinkDatabase.IBoughtDrinkDatabaseService>();
            _drinkBrandDatabase = Ioc.Resolve<Database.Services.DrinkBrandDatabase.IDrinkBrandDatabaseService>();
        }

        public async Task MigrateAll()
        {
            await MigrateUsers();
            await MigrateDrinks();
            await MigrateBoughtDrinks();
            await MigrateDrinkBrands();
        }

        private async Task MigrateUsers()
        {
            var oldUsers = await _oldUserDatabase.GetUsers();

            foreach (var oldUser in oldUsers)
            {
                await _userDatabase.Create(new UserModel
                {
                    Id = oldUser.Id,
                    Name = oldUser.Name,
                    ImagePath = oldUser.ImagePath,
                    Role = oldUser.Role,
                    DateCreated = oldUser.DateCreated,
                    DateModified = oldUser.DateModified
                });
            }

            Logger.Success("Successfully Migrated Users");
        }

        private async Task MigrateDrinks()
        {
            var drinks = await _oldDrinkDatabase.GetDrinks();

            foreach (var drink in drinks)
            {
                await _drinkDatabase.Create(new DrinkModel
                {
                    Id = drink.Id,
                    Name = drink.Name,
                    ImagePath = drink.ImagePath,
                    AlcoholContent = drink.AlcoholContent,
                    Price = drink.Price,
                    AdminPrice = drink.AdminPrice,
                    Quantity = drink.Quantity,
                    BrandIds = drink.BrandId,
                    Type = drink.Type,
                    DateCreated = drink.DateCreated,
                    DateModified = drink.DateModified
                });
            }

            Logger.Success("Successfully Migrated Drinks");
        }

        private async Task MigrateBoughtDrinks()
        {
            var drinks = await _oldBoughtDrinksDatabase.GetAll();

            foreach (var drink in drinks)
            {
                await _boughtDrinksDatabase.Create(new BoughtDrinkModel
                {
                    Id = drink.Id,
                    UserId = drink.UserId,
                    DrinkId = drink.DrinkId,
                    Quantity = drink.Quantity,
                    FullPrice = drink.FullPrice,
                    DrinkName = drink.DrinkName,
                    DatePurchased = drink.DatePurchased,
                });
            }

            Logger.Success("Successfully Migrated Bought Drinks");
        }


        private async Task MigrateDrinkBrands()
        {
            var brands = await _oldDrinkBrandDatabase.GetDrinkBrands();

            foreach (var brand in brands)
            {
                await _drinkBrandDatabase.Create(new DrinkBrandModel
                {
                    Id = brand.Id,
                    Name = brand.Name,

                });
            }

            Logger.Success("Successfully Migrated Drink Brands");
        }
    }
}
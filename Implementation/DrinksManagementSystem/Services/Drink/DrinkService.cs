using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Services.DrinksDatabase;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.DrinkBrand;
using DrinksManagementSystem.Services.Storage;

namespace DrinksManagementSystem.Services.Drink
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkDatabaseService _databaseService;
        private readonly IBoughtDrinkService _boughtDrinkService;
        private readonly IStorageService _storageService;
        private readonly IDrinkBrandService _brandService;

        public ObservableCollection<Entities.Drink> Drinks { get; set; } = new ObservableCollection<Entities.Drink>();

        public DrinkService(
            IDrinkDatabaseService drinkDatabaseService,
            IBoughtDrinkService boughtDrinkService,
            IStorageService storageService,
            IDrinkBrandService brandService
        )
        {
            _databaseService = drinkDatabaseService;
            _boughtDrinkService = boughtDrinkService;
            _storageService = storageService;
            _brandService = brandService;
        }

        public ObservableCollection<Entities.Drink> GetAll()
        {
            var drinks = _databaseService.GetAll();

            if (drinks == null) return null;

            Drinks.Clear();

            foreach (var drink in drinks.Select(drinkDto => new Entities.Drink(drinkDto)))
            {
                if (drink.BrandIds != null)
                {
                    foreach (var brandId in drink.BrandIds)
                    {
                        drink.Brands.Add(_brandService.Get(brandId));
                    }
                }

                Drinks.Add(drink);
            }

            return Drinks;
        }

        public Entities.Drink Get(int id)
        {
            if (Drinks?.Count > 0)
            {
                return Drinks.FirstOrDefault(drink => drink.Id == id);
            }

            var drinkDto = _databaseService.Get(id);
            var drink = new Entities.Drink(drinkDto);

            foreach (var brandId in drink.BrandIds)
            {
                drink.Brands.Add(_brandService.Get(brandId));
            }


            return drink;
        }


        public Entities.BoughtDrink[] ApplyFromBoughtDrinks(Entities.BoughtDrink[] drinks)
        {
            foreach (var boughtDrink in drinks)
            {
                boughtDrink.Drink = Get(boughtDrink.DrinkId);
            }

            return drinks;
        }

        public async Task<bool> Create(Entities.Drink drink)
        {
            try
            {
                var newId = await _databaseService.Create(drink.ToDto());

                if (newId == -1) return false;

                drink.Id = (int) newId;
                Drinks.Add(drink);

                return true;
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }

            return false;
        }

        public async Task<bool> Update(Entities.Drink drink)
        {
            try
            {
                var result = await _databaseService.Update(drink.ToDto());

                if (!result) return false;

                var drinkFromList = Drinks.FirstOrDefault(u => u.Id == drink.Id);
                var index = Drinks.IndexOf(drinkFromList);

                if (index >= 0)
                {
                    Drinks[index] = drink;
                }

                return true;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

            return false;
        }

        public async Task<bool> Remove(Entities.Drink drink)
        {
            if (drink.ImagePath != null)
            {
                await _storageService.RemovePicture(drink.ImagePath);
            }

            var result = await _databaseService.Remove(drink.Id);

            if (!result) return false;

            var index = Drinks.IndexOf(Drinks.First(u => u.Id == drink.Id));
            if (index >= 0)
            {
                Drinks.RemoveAt(index);
            }

            return true;
        }

        public async Task<bool> Buy(Entities.User user, Entities.Drink drink, int quantity, double price)
        {
            var result = await _boughtDrinkService.Create(new Entities.BoughtDrink()
            {
                UserId = user.Id,
                DrinkId = drink.Id,
                DrinkName = drink.Name,
                Quantity = quantity,
                FullPrice = price,
                DatePurchased = DateTime.UtcNow
            });

            if (!result) return false;

            if (drink.Quantity is > 0)
            {
                drink.Quantity -= quantity;

                await Update(drink);
            }

            return true;
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Services.BoughtDrinkDatabase;
using Database.Services.DrinksDatabase;
using DrinksManagementSystem.Entities;
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

        public void Start()
        {
            _databaseService.Start();
        }

        public async Task<ObservableCollection<Entities.Drink>> GetAll()
        {
            var drinks = await _databaseService.GetDrinks();

            Drinks.Clear();

            foreach (var drink in drinks.Select(drinkDto => new Entities.Drink(drinkDto)))
            {
                if (drink.BrandId != null)
                {
                    drink.Brand = await _brandService.Get(drink.BrandId);
                }
                Drinks.Add(drink);
            }

            return Drinks;
        }

        public async Task<Entities.Drink> Get(int id)
        {
            var drinkDto = await _databaseService.GetDrink(id);
            var drink = new Entities.Drink(drinkDto);
            drink.Brand = await _brandService.Get(drink.BrandId);

            return drink;
        }

        public async Task<bool> Create(Entities.Drink drink)
        {
            try
            {
                var newId = await _databaseService.CreateDrink(drink.ToDto());

                if (newId == null) return false;

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
                var result = await _databaseService.UpdateDrink(drink.ToDto());

                if (result < 0) return false;

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

        public async Task<int> Remove(Entities.Drink drink)
        {
            if (drink.ImagePath != null)
            {
                await _storageService.RemovePicture(drink.ImagePath);
            }

            var result = await _databaseService.RemoveDrink(drink.Id);

            if (result < 0) return result;

            var index = Drinks.IndexOf(Drinks.First(u => u.Id == drink.Id));
            if (index >= 0)
            {
                Drinks.RemoveAt(index);
            }

            return result;
        }

        public async Task<bool> Buy(Entities.User user, Entities.Drink drink, int quantity, double price)
        {
            var result = await _boughtDrinkService.Create(new Entities.BoughtDrink()
            {
                UserId = user.Id,
                DrinkId = drink.Id,
                DrinkName = drink.Name,
                Quantity = quantity,
                FullPrice = price
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
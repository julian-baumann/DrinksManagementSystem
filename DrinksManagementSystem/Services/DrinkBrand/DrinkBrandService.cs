using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Services.DrinkBrandDatabase;
using DrinksManagementSystem.Services.Storage;

namespace DrinksManagementSystem.Services.DrinkBrand
{
    public class DrinkBrandService : IDrinkBrandService
    {
        private readonly IDrinkBrandDatabaseService _brandDatabaseService;
        private readonly IStorageService _storageService;

        public ObservableCollection<Entities.DrinkBrand> Brands { get; set; } = new ObservableCollection<Entities.DrinkBrand>();

        public DrinkBrandService(
            IDrinkBrandDatabaseService drinkBrandDatabaseService,
            IStorageService storageService
        )
        {
            _brandDatabaseService = drinkBrandDatabaseService;
            _storageService = storageService;
        }

        public void Start()
        {
            _brandDatabaseService.Start();
        }

        public async Task<ObservableCollection<Entities.DrinkBrand>> GetAll()
        {
            var drinks = await _brandDatabaseService.GetDrinkBrands();

            Brands.Clear();

            foreach (var drinkDto in drinks)
            {
                Brands.Add(new Entities.DrinkBrand(drinkDto));
            }

            return Brands;
        }

        public async Task<Entities.DrinkBrand> Get(string id)
        {
            if (Brands?.Count > 0)
            {
                return Brands.First((brand) => brand.Id == id);
            }

            var brandDto = await _brandDatabaseService.GetDrinkBrand(id);
            return new Entities.DrinkBrand(brandDto);
        }

        public async Task<int> Create(Entities.DrinkBrand drink)
        {
            try
            {
                var result = await _brandDatabaseService.CreateDrinkBrand(drink.ToDto());

                if (result >= 0)
                {
                    Brands.Add(drink);
                }

                return result;
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }

            return -1;
        }

        public async Task<int> Update(Entities.DrinkBrand brand)
        {
            try
            {
                var result = await _brandDatabaseService.UpdateDrinkBrand(brand.ToDto());

                if (result < 0) return result;

                var drinkFromList = Brands.FirstOrDefault(u => u.Id == brand.Id);
                var index = Brands.IndexOf(drinkFromList);

                if (index >= 0)
                {
                    Brands[index] = brand;
                }

                return result;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

            return -1;
        }

        public async Task<int> Remove(Entities.DrinkBrand brand)
        {
            var result = await _brandDatabaseService.RemoveDrinkBrand(brand.Id);

            if (result < 0) return result;

            var index = Brands.IndexOf(Brands.FirstOrDefault(u => u.Id == brand.Id));
            Brands.RemoveAt(index);

            return result;
        }
    }
}
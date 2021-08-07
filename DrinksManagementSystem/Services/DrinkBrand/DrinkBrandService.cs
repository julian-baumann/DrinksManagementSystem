using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Services.DrinkBrandDatabase;

namespace DrinksManagementSystem.Services.DrinkBrand
{
    public class DrinkBrandService : IDrinkBrandService
    {
        private readonly IDrinkBrandDatabaseService _brandDatabaseService;

        public ObservableCollection<Entities.DrinkBrand> Brands { get; set; } = new ObservableCollection<Entities.DrinkBrand>();

        public DrinkBrandService(
            IDrinkBrandDatabaseService drinkBrandDatabaseService
        )
        {
            _brandDatabaseService = drinkBrandDatabaseService;
        }

        public ObservableCollection<Entities.DrinkBrand> GetAll()
        {
            var drinks = _brandDatabaseService.GetDrinkBrands();

            if (drinks == null) return null;

            Brands.Clear();

            foreach (var drinkDto in drinks)
            {
                Brands.Add(new Entities.DrinkBrand(drinkDto));
            }

            return Brands;
        }

        public Entities.DrinkBrand Get(string id)
        {
            if (Brands?.Count > 0)
            {
                return Brands.First((brand) => brand.Id == id);
            }

            var brandDto = _brandDatabaseService.GetDrinkBrand(id);
            return new Entities.DrinkBrand(brandDto);
        }

        public async Task<string> Create(Entities.DrinkBrand drink)
        {
            try
            {
                var result = await _brandDatabaseService.CreateDrinkBrand(drink.ToDto());

                if (result != null)
                {
                    Brands.Add(drink);
                }

                return null;
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }

            return null;
        }

        public async Task<bool> Update(Entities.DrinkBrand brand)
        {
            try
            {
                var result = await _brandDatabaseService.UpdateDrinkBrand(brand.ToDto());

                if (!result) return false;

                var drinkFromList = Brands.FirstOrDefault(u => u.Id == brand.Id);
                var index = Brands.IndexOf(drinkFromList);

                if (index >= 0)
                {
                    Brands[index] = brand;
                }

                return true;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

            return false;
        }

        public async Task<bool> Remove(string id)
        {
            var result = await _brandDatabaseService.RemoveDrinkBrand(id);

            if (!result) return false;

            var index = Brands.IndexOf(Brands.FirstOrDefault(u => u.Id == id));
            Brands.RemoveAt(index);

            return true;
        }
    }
}
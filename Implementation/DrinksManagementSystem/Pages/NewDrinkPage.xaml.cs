using System;
using System.Collections.Generic;
using System.Globalization;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.Drink;
using DrinksManagementSystem.Services.DrinkBrand;
using DrinksManagementSystem.Services.Storage;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDrinkPage : ContentPage
    {
        private IDrinkBrandService _drinkBrandService;
        private IDrinkService _drinkService;
        private IStorageService _storageService;

        private FileBase _imageFile;

        public bool IsNewDrink { get; set; }

        public Drink Drink { get; set; } = new Drink();

        public string Price { get => GetPrice(Drink.Price); set => Drink.Price = SetPrice(value); }
        public string AdminPrice { get => GetPrice(Drink.AdminPrice); set => Drink.AdminPrice = SetPrice(value); }
        public string FlatPrice { get => GetPrice(Drink.FlatPrice); set => Drink.FlatPrice = SetPrice(value); }
        public string AlcoholContent { get => GetPrice(Drink.AlcoholContent); set => Drink.AlcoholContent = SetPrice(value); }

        public IList<string> DrinkTypes { get; set; } = new List<string>()
        {
            "-",
            "Bier",
            "Wein",
            "Mischgetränke",
            "Schnaps",
            "Antialkohol",
            "Cocktails"
        };

        public NewDrinkPage()
        {
            IsNewDrink = true;
            Initialize();
            BindingContext = this;
            InitializeComponent();
        }

        public NewDrinkPage(Drink drink)
        {
            Drink = drink.Clone();

            Initialize();

            if (Drink.BrandIds != null)
            {
                Drink.Brands.Clear();

                foreach (var brandId in Drink.BrandIds)
                {
                    Drink.Brands.Add(_drinkBrandService.Get(brandId));
                }
            }

            BindingContext = this;
            InitializeComponent();

            if (drink.Type != null)
            {
                var index = DrinkTypes.IndexOf(drink.Type);
                typePicker.SelectedIndex = index;
            }

            Title = "Details";
            editPictureView.ImagePath = Drink.ImagePath;
        }

        private void Initialize()
        {
            _storageService = Ioc.Resolve<IStorageService>();
            _drinkService = Ioc.Resolve<IDrinkService>();
            _drinkBrandService = Ioc.Resolve<IDrinkBrandService>();
        }


        private static string GetPrice(double? value)
        {
            if (value == null) return null;

            return value.ToString().Replace(".", ",");
        }

        private static double? SetPrice(string value)
        {
            if (value is not {Length: > 0}) { return null; }

            try
            {
                return !value.Contains(",") ? double.Parse(value) : double.Parse(value, new NumberFormatInfo() { NumberDecimalSeparator = "," });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private async void OnAddDrinkClicked(object sender, EventArgs e)
        {
            if (Drink.Name == null || Drink.Name.Length <= 0)
            {
                await DisplayAlert("Fehler", "Bitte name eintragen", "Ok");
                return;
            }

            if (Drink.Type == "-")
            {
                Drink.Type = null;
            }

            if (_imageFile != null)
            {
                if (Drink.ImagePath != null)
                {
                    await _storageService.RemovePicture(Drink.ImagePath);
                }

                using var stream = await _imageFile.OpenReadAsync();

                var newImagePath = await _storageService.StorePicture(_imageFile.FileName, stream);
                Drink.ImagePath = newImagePath;
            }

            Drink.DateCreated = DateTime.Now;

            bool result;

            if (IsNewDrink)
            {
                result = await _drinkService.Create(Drink);
            }
            else
            {
                result = await _drinkService.Update(Drink);
            }

            if (result)
            {
                Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok :(");
            }
        }

        private void OnImageChanged(object sender, FileBase file)
        {
            _imageFile = file;
        }

        private async void OnAddBrandClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageDrinkBrandsPage(Drink));
        }

        private async void OnRemoveDrinkClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Löschen", "Bist du dir sicher?", "Ja", "Nein");
            if (!answer) return;

            var result = await _drinkService.Remove(Drink);

            if (result)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok");
            }
        }
    }
}
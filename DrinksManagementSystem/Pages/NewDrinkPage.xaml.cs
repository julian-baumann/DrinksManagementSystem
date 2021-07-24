using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.Drink;
using DrinksManagementSystem.Services.DrinkBrand;
using DrinksManagementSystem.Services.Storage;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

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
        }

        public NewDrinkPage(Drink drink)
        {
            Drink = drink.Clone();
            Initialize();
            editPictureView.ImagePath = Drink.ImagePath;
            InitializeExistingDrink(drink);
        }

        private void Initialize()
        {
            _storageService = Ioc.Resolve<IStorageService>();
            _drinkService = Ioc.Resolve<IDrinkService>();
            _drinkBrandService = Ioc.Resolve<IDrinkBrandService>();

            InitializeComponent();
            BindingContext = this;

            Title = "Details";
        }

        private async Task InitializeExistingDrink(Drink drink)
        {
            if (drink.Type != null)
            {
                var index = DrinkTypes.IndexOf(drink.Type);
                typePicker.SelectedIndex = index;
            }

            if (Drink.Brand == null && Drink.BrandId != null)
            {
                Drink.Brand = await _drinkBrandService.Get(Drink.BrandId);
            }

            brandButton.Text = Drink?.Brand?.Name ?? "Wählen";
        }


        private static string GetPrice(double? value)
        {
            if (value == null) return null;

            return value.ToString().Replace(".", ",");
        }

        private static double? SetPrice(string value)
        {
            if (value == null || value.Length <= 0) return null;
            try
            {
                if (!value.Contains(",")) return double.Parse(value);

                return double.Parse(value, new NumberFormatInfo() { NumberDecimalSeparator = "," });;

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

        private async void OnChooseBrandClicked(object sender, EventArgs e)
        {
            var choosePage = new DrinkBrandChooserPage();
            choosePage.Brand += (o, brand) =>
            {
                Drink.Brand = brand;
                brandButton.Text = brand.Name;
            };

            var page = new NavigationPage(choosePage);
            page.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);
            await Navigation.PushModalAsync(page);

        }

        private async void OnRemoveDrinkClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Löschen", "Bist du dir sicher?", "Ja", "Nein");
            if (!answer) return;

            var result = await _drinkService.Remove(Drink);

            if (result >= 0)
            {
                Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok");
            }
        }
    }
}
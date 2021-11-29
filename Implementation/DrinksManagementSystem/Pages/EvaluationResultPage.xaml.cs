using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.Drink;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluationResultPage : ContentPage
    {
        private readonly IDrinkService _drinkService;
        private readonly IBoughtDrinkService _bougthDrinkService;

        private BoughtDrink[] _boughtDrinks;
        private HashSet<EvaluationDrinkInfo> _boughtDrinksCombined = new();

        public HashSet<EvaluationDrinkInfo> BoughtDrinksCombined
        {
            get => _boughtDrinksCombined;
            set
            {
                _boughtDrinksCombined = value;
                OnPropertyChanged();
            }
        }

        public double FlatPrice { get; set; }
        public double UsersCount { get; set; }
        public double AllDrinksPrice { get; set; }
        public double FlatSum { get; set; }
        public double Profit { get; set; }
        public GeneralNotifiable UIInfo { get; set; } = new GeneralNotifiable();

        public EvaluationResultPage(BoughtDrink[] boughtDrinks, double flatPrice)
        {
            _boughtDrinks = boughtDrinks;
            FlatPrice = flatPrice;
            _drinkService = Ioc.Resolve<IDrinkService>();
            _bougthDrinkService = Ioc.Resolve<IBoughtDrinkService>();

            GenerateItems();

            InitializeComponent();
            BindingContext = this;
        }

        private void GenerateItems()
        {
            try
            {
                var users = new HashSet<int>();

                foreach (var boughtDrink in _boughtDrinks)
                {
                    users.Add(boughtDrink.UserId);

                    var existingItem = BoughtDrinksCombined.FirstOrDefault(element => element.DrinkId == boughtDrink.DrinkId);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += boughtDrink.Quantity;
                        existingItem.FullPrice += boughtDrink.FullPrice;
                    }
                    else
                    {
                        var drink = _drinkService.Get(boughtDrink.DrinkId);

                        var item = new EvaluationDrinkInfo
                        {
                            DrinkId = boughtDrink.DrinkId,
                            FullPrice = boughtDrink.FullPrice,
                            Quantity = boughtDrink.Quantity,
                            Drink = drink
                        };

                        BoughtDrinksCombined.Add(item);
                    }

                    AllDrinksPrice += boughtDrink.FullPrice;
                }

                UsersCount = users.Count;
                FlatSum = (UsersCount * FlatPrice);
                Profit = AllDrinksPrice - FlatSum;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public async void OnDeleteClicked(System.Object sender, System.EventArgs e)
        {
            try
            {

                var answer = await DisplayAlert("Alle gekauften Getränke löschen", "Bist du dir sicher?", "Ja", "Nein");

                if (!answer) { return; };

                UIInfo.Loading = true;


                foreach (var boughtDrink in _boughtDrinks)
                {
                    await _bougthDrinkService.Remove(boughtDrink.Id);
                }

                UIInfo.Loading = false;

                await Navigation.PopAsync();
            }
            catch(Exception exception)
            {
                await DisplayAlert("Fehler", exception.ToString(), "Ok");
            }
        }
    }
}
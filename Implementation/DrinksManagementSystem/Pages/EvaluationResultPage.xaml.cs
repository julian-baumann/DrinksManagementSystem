using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.Drink;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluationResultPage : ContentPage
    {
        private readonly IDrinkService _drinkService;

        private HashSet<EvaluationDrinkInfo> _boughtDrinks = new();

        public HashSet<EvaluationDrinkInfo> BoughtDrinks
        {
            get => _boughtDrinks;
            set
            {
                _boughtDrinks = value;
                OnPropertyChanged();
            }
        }

        public double FlatPrice { get; set; }
        public double UsersCount { get; set; }
        public double AllDrinksPrice { get; set; }
        public double DrinksMinusFlatPrice { get; set; }

        public EvaluationResultPage(BoughtDrink[] boughtDrinks, double flatPrice)
        {
            FlatPrice = flatPrice;
            _drinkService = Ioc.Resolve<IDrinkService>();

            GenerateItems(boughtDrinks);

            InitializeComponent();
            BindingContext = this;
        }

        private void GenerateItems(IEnumerable<BoughtDrink> boughtDrinks)
        {
            try
            {
                var users = new HashSet<int>();

                foreach (var boughtDrink in boughtDrinks)
                {
                    users.Add(boughtDrink.UserId);

                    var existingItem = BoughtDrinks.FirstOrDefault(element => element.DrinkId == boughtDrink.DrinkId);

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

                        BoughtDrinks.Add(item);
                    }

                    AllDrinksPrice += boughtDrink.FullPrice;
                }

                UsersCount = users.Count;
                DrinksMinusFlatPrice = AllDrinksPrice - (UsersCount * FlatPrice);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
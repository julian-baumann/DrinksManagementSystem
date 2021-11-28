using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.Drink;
using DrinksManagementSystem.Services.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlatPage : ContentPage
    {
        private readonly IDrinkService _drinkService;
        private readonly IUserService _userService;
        private readonly IBoughtDrinkService _boughtDrinkService;


        public ObservableCollection<EvaluationDrinkInfo> Drinks { get; set; } = new();

        public FlatPage()
        {
            _drinkService = Ioc.Resolve<IDrinkService>();
            _userService = Ioc.Resolve<IUserService>();
            _boughtDrinkService = Ioc.Resolve<IBoughtDrinkService>();


            var drinks = _drinkService.GetAll();

            foreach (var drink in drinks)
            {
                if (drink.FlatPrice is not null or 0)
                {
                    Drinks.Add(new EvaluationDrinkInfo
                    {
                        Drink = drink,
                        DrinkId = drink.Id,
                        DrinkName = drink.Name,
                        Quantity = 0,
                        FullPrice = 0
                    });
                }
            }

            BindingContext = this;
            InitializeComponent();
        }

        public void IncreaseQuantity(object sender, EventArgs eventArgs)
        {
            if (((ImageButton) sender).BindingContext is EvaluationDrinkInfo drink)
            {
                drink.Quantity += 1;
                drink.FullPrice += (double) drink.Drink.FlatPrice!;
            }
        }

        public void DecreaseQuantity(object sender, EventArgs eventArgs)
        {
            if (((ImageButton) sender).BindingContext is EvaluationDrinkInfo drink)
            {
                if (drink.Quantity > 0)
                {
                    drink.Quantity -= 1;
                    drink.FullPrice -= (double) drink.Drink.FlatPrice!;
                }
            }
        }
    }
}
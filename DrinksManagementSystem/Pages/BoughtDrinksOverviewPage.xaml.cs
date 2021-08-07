using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BoughtDrinksOverviewPage : ContentPage
    {
        private readonly IBoughtDrinkService _boughtDrinkService;

        public ObservableCollection<BoughtDrink> BoughtDrinks { get; set; }

        public BoughtDrinksOverviewPage(ObservableCollection<BoughtDrink> boughtDrinks)
        {
            _boughtDrinkService = Ioc.Resolve<IBoughtDrinkService>();

            BoughtDrinks = boughtDrinks;

            BindingContext = this;
            InitializeComponent();
        }

        private async void Pay(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                var preParent = (StackLayout)button.Parent;
                var listViewItem = (StackLayout)preParent.Parent;
                var label = (Label)listViewItem.Children[0];

                var id = int.Parse(label.Text);
                var result = await _boughtDrinkService.Remove(id);

                if (!result) return;

                var drink = BoughtDrinks.FirstOrDefault(drink => drink.Id == id);
                BoughtDrinks.Remove(drink);

            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

        }
    }
}
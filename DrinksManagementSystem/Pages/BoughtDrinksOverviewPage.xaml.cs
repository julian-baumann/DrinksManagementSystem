using System;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.BoughtDrink;
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
                var drink = BoughtDrinks.FirstOrDefault(drink => drink.Id == id);

                if (drink != null)
                {
                    drink.DatePayed = DateTime.UtcNow;
                    var result = await _boughtDrinkService.Update(drink);

                    if (!result) return;

                    BoughtDrinks.Remove(drink);
                }
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok :(");
            }
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);

            if (menuItem?.CommandParameter is BoughtDrink drink)
            {
                var result = await _boughtDrinkService.Remove(drink.Id);
                if (result)
                {
                    BoughtDrinks.Remove(drink);
                }
                else
                {
                    await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok :(");
                }
            }
        }
    }
}
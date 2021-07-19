using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ObservableCollection<BoughtDrink> BoughtDrinks { get; set; } = new ObservableCollection<BoughtDrink>();

        public BoughtDrinksOverviewPage(BoughtDrink[] boughtDrinks)
        {
            _boughtDrinkService = Ioc.Resolve<IBoughtDrinkService>();

            BoughtDrinks = new ObservableCollection<BoughtDrink>(boughtDrinks);

            BindingContext = this;
            InitializeComponent();
        }

        private void Pay(object sender, EventArgs e)
        {
        }
    }
}
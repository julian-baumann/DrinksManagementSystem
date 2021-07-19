using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DrinksPage : ContentPage
    {
        private readonly IDrinkService _drinkService;

        public ObservableCollection<Drink> Drinks { get; set; } = new ObservableCollection<Drink>();

        public DrinksPage()
        {
            _drinkService = Ioc.Resolve<IDrinkService>();
            Initialize();

        }

        private async Task Initialize()
        {
            await _drinkService.GetAll();
            Drinks = _drinkService.Drinks;
            BindingContext = this;
            InitializeComponent();
        }

        private void OnAddNewDrinkClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewDrinkPage());
        }

        private void OnItemSelectionChange(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                Navigation.PushAsync(new NewDrinkPage(e.Item as Drink));
            }
        }
    }
}
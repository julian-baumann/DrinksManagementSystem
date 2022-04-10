using System.Collections.ObjectModel;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.Drink;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkChooserPage : ContentPage
    {
        private readonly IDrinkService _drinkService;

        public User SelectedUser { get; set; }
        public bool IsAdmin { get; set; } = false;
        public ObservableCollection<Drink> Drinks { get; set; }

        public DrinkChooserPage(User user)
        {
            _drinkService = Ioc.Resolve<IDrinkService>();

            Drinks = _drinkService.Drinks;
            SelectedUser = user;
            BindingContext = this;
            IsAdmin = SelectedUser.Role == UserRoles.Admin;

            InitializeComponent();
        }

        private void OnDrinkClicked(object sender, Drink drink)
        {
            var buyDrinkPage = new BuyDrinkPage(SelectedUser, drink);
            buyDrinkPage.OnClose += (_, _) =>
            {
                Navigation.PopAsync();
            };
            var page = new NavigationPage(buyDrinkPage);
            page.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);
            Navigation.PushModalAsync(page);
        }
    }
}
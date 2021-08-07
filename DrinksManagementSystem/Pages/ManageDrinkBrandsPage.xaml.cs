using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksManagementSystem.Entities;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageDrinkBrandsPage : ContentPage
    {
        public Drink Drink { get; set; }

        public ManageDrinkBrandsPage(Drink drink)
        {
            Drink = drink;
            BindingContext = this;
            InitializeComponent();
        }

        private async void OnAddBrandClicked(object sender, EventArgs e)
        {
            var choosePage = new DrinkBrandChooserPage();
            choosePage.Brand += (o, brand) =>
            {
                Drink.Brands.Add(brand);
            };

            var page = new NavigationPage(choosePage);
            page.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);
            await Navigation.PushModalAsync(page);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);

            if (menuItem?.CommandParameter is DrinkBrand brand)
            {
                Drink.Brands.Remove(brand);
            }
        }
    }
}
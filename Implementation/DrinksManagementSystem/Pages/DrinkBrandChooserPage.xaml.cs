using System;
using System.Collections.ObjectModel;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.DrinkBrand;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkBrandChooserPage : ContentPage
    {
        private readonly IDrinkBrandService _brandService;

        public ObservableCollection<DrinkBrand> Brands { get; set; } = new ObservableCollection<DrinkBrand>();

        public EventHandler<DrinkBrand> Brand = delegate { };

        public DrinkBrandChooserPage()
        {
            _brandService = Ioc.Resolve<IDrinkBrandService>();

            Brands = _brandService.GetAll();

            InitializeComponent();

            BindingContext = this;
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void OnItemTap(object sender, ItemTappedEventArgs e)
        {
            if (e?.Item != null)
            {
                Brand.Invoke(this, e.Item as DrinkBrand);
                Navigation.PopModalAsync();
            }
        }

        private async void OnAddNewBrandClicked(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("Name", "Name der Marke");

            if (result == null) return;

            await _brandService.Create(new DrinkBrand()
            {
                Name = result
            });
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);

            if (menuItem?.CommandParameter is DrinkBrand brand)
            {
                _brandService.Remove(brand.Id);
            }
        }
    }
}
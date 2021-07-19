using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Initialize();
        }

        private async Task Initialize()
        {
            Brands = await _brandService.GetAll();

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
            string result = await DisplayPromptAsync("Name", "Name der Marke");

            if (result == null) return;

            _brandService.Create(new DrinkBrand() { Name = result });
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);

            if (menuItem?.CommandParameter is DrinkBrand brand)
            {
                _brandService.Remove(brand);
            }
        }
    }
}
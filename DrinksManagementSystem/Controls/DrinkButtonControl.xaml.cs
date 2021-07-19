using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.DrinkBrand;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkButtonControl : ContentView
    {
        private readonly IDrinkBrandService _brandService;

        public static readonly BindableProperty DrinkProperty = BindableProperty.Create(
            propertyName: nameof(Drink),
            returnType: typeof(Drink),
            declaringType: typeof(DrinkButtonControl),
            defaultValue: new Drink(),
            defaultBindingMode: BindingMode.TwoWay
        );

        public Drink Drink
        {
            get => (Drink)GetValue(DrinkProperty);
            set => SetValue(DrinkProperty, value);
        }

        public event EventHandler<Drink> Clicked = delegate { };

        public DrinkButtonControl()
        {
            _brandService = Ioc.Resolve<IDrinkBrandService>();
            BindingContext = this;
            InitializeComponent();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Clicked.Invoke(this, Drink);
        }
    }
}
using System;
using System.Diagnostics;
using System.Windows.Input;
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
        public static readonly BindableProperty DrinkProperty = BindableProperty.Create(
            propertyName: nameof(Drink),
            returnType: typeof(Drink),
            declaringType: typeof(DrinkButtonControl),
            defaultValue: new Drink(),
            defaultBindingMode: BindingMode.OneWay
        );

        public Drink Drink
        {
            get => (Drink)GetValue(DrinkProperty);
            set => SetValue(DrinkProperty, value);
        }

        public ICommand Tapped { get; set; }

        public event EventHandler<Drink> Clicked = delegate { };

        public DrinkButtonControl()
        {
            Tapped = new Command(() => OnClicked(null, null), () => IsEnabled);

            BindingContext = this;
            InitializeComponent();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            if (Drink.Quantity > 0)
            {
                Clicked.Invoke(this, Drink);
            }
        }
    }
}
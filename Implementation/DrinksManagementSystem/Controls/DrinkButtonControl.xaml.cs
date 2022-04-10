using System;
using System.Windows.Input;
using DrinksManagementSystem.Entities;
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

        public static readonly BindableProperty IsAdminProperty = BindableProperty.Create(
            propertyName: nameof(IsAdmin),
            returnType: typeof(bool),
            declaringType: typeof(DrinkButtonControl),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: IsAdminPropertyChanged
        );

        private static void IsAdminPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (DrinkButtonControl) bindable;
            var priceLabel = control.FindByName<Label>("PriceLabel");
            var adminPriceLabel = control.FindByName<Label>("AdminPriceLabel");

            var value = (bool) newvalue;

            priceLabel.IsVisible = !value;
            adminPriceLabel.IsVisible = value;
        }

        public Drink Drink
        {
            get => (Drink)GetValue(DrinkProperty);
            set => SetValue(DrinkProperty, value);
        }

        public bool IsAdmin { get; set; }

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
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Pages;
using DrinksManagementSystem.Services.BoughtDrink;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserUnpaidInfoControl : ContentView, INotifyPropertyChanged
    {
        private readonly IBoughtDrinkService _boughtDrinkService;

        private ObservableCollection<BoughtDrink> _boughtDrinks;

        public static readonly BindableProperty UserIdProperty = BindableProperty.Create(
            propertyName: nameof(UserId),
            returnType: typeof(int),
            declaringType: typeof(UserUnpaidInfoControl),
            defaultValue: -1,
            defaultBindingMode: BindingMode.TwoWay
        );

        public int UserId
        {
            get => (int)GetValue(UserIdProperty);
            set
            {
                SetValue(UserIdProperty, value);
                Initialize(value);
            }
        }

        private double _totalCosts;
        public double TotalCosts { get => _totalCosts; set => Set(ref _totalCosts, value); }

        public UserUnpaidInfoControl()
        {
            _boughtDrinkService = Ioc.Resolve<IBoughtDrinkService>();

            BindingContext = this;
            InitializeComponent();
        }

        private void Initialize(int? id)
        {
            if (id is < 0) return;

            _boughtDrinks = new ObservableCollection<BoughtDrink>(_boughtDrinkService.GetAllUnpaidDrinksByUser((int)id));

            _boughtDrinks.CollectionChanged += (sender, args) =>
            {
                CalculateFullPrice();
            };

            CalculateFullPrice();
        }

        private void CalculateFullPrice()
        {
            TotalCosts = 0;

            foreach (var boughtDrink in _boughtDrinks)
            {
                TotalCosts += boughtDrink.FullPrice;
            }

            if (TotalCosts > 0)
            {
                container.IsVisible = true;
            }
        }

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BoughtDrinksOverviewPage(_boughtDrinks));
        }
    }
}
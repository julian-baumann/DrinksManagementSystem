using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class BuyDrinkPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IDrinkService _drinkService;

        public User SelectedUser { get; set; }
        public Drink SelectedDrink { get; set; }

        private int _quantity = 1;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                Set(ref _quantity, value);

                if (SelectedUser.Role == UserRoles.Admin)
                {
                    if (SelectedDrink.AdminPrice is > 0)
                    {
                        Price = (double)SelectedDrink.AdminPrice * Quantity;
                    }
                    else if (SelectedDrink.Price is > 0)
                    {
                        Price = (double)SelectedDrink.Price * Quantity;
                    }
                }
                else if (SelectedUser.Role == UserRoles.Guest && SelectedDrink.Price is > 0)
                {
                    Price = (double)SelectedDrink.Price * Quantity;
                }
            }
        }

        private double _price;
        public double Price { get => _price; set => Set(ref _price, value); }

        public BuyDrinkPage(User user, Drink drink)
        {
            _drinkService = Ioc.Resolve<IDrinkService>();

            SelectedUser = user;
            SelectedDrink = drink;

            if (SelectedUser.Role == UserRoles.Admin)
            {
                if (SelectedDrink.AdminPrice is > 0)
                {
                    Price = (double)SelectedDrink.AdminPrice;
                }
                else if (SelectedDrink.Price is > 0)
                {
                    Price = (double)SelectedDrink.Price;
                }
            }
            else if (SelectedUser.Role == UserRoles.Guest && SelectedDrink.Price is > 0)
            {
                Price = (double)SelectedDrink.Price;
            }

            BindingContext = this;
            InitializeComponent();
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }


        public void Remove(object sender, EventArgs eventArgs)
        {
            if (Quantity > 1)
            {
                Quantity--;
            }
        }

        public void Add(object sender, EventArgs eventArgs)
        {
            if (SelectedDrink.Quantity == null)
            {
                Quantity++;
            }
            else if (Quantity < SelectedDrink.Quantity)
            {
                Quantity++;
            }
        }

        private async void OnBuyClicked(object sender, EventArgs e)
        {
            baseContent.IsVisible = false;
            loadingScreen.IsVisible = true;
            var result = await _drinkService.Buy(SelectedUser, SelectedDrink, Quantity, Price);

            loadingScreen.IsVisible = false;

            if (result)
            {
                doneScreen.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok");
                baseContent.IsVisible = true;
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

    }
}
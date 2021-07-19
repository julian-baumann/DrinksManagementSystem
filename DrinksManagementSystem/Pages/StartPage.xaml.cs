using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        private readonly IUserService _userService;

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public StartPage()
        {
            _userService = Ioc.Resolve<IUserService>();
            Users = _userService.Users;

            InitializeComponent();
            BindingContext = this;
        }

        private void OnUserClicked(object sender, User user)
        {
            if (user == null) return;

            Navigation.PushAsync(new DrinkChooserPage(user));
        }
    }
}
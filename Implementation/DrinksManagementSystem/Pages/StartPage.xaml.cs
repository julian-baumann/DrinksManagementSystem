using System;
using System.Collections.ObjectModel;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.User;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

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

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            var page = new NavigationPage(new SettingsPage());
            page.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);
            Navigation.PushModalAsync(page);
        }
    }
}
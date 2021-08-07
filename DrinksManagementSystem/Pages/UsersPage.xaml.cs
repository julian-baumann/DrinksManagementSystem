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
    public partial class UsersPage : ContentPage
    {
        private readonly IUserService _userService;

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public UsersPage()
        {
            _userService = Ioc.Resolve<IUserService>();

            _userService.GetAll();
            Users = _userService.Users;
            BindingContext = this;
            InitializeComponent();
        }

        private void OnAddNewUserClicked(object sender, EventArgs e)
        {
            var page = new NavigationPage(new NewUserPage());
            page.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);
            Navigation.PushModalAsync(page);
        }

        private void OnItemSelectionChange(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                Navigation.PushAsync(new UserDetailPage(e.Item as User));
            }
        }
    }
}
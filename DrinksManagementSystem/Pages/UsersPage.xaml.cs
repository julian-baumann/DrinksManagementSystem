using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core;
using Database.Entities;
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

        public ObservableCollection<User> Users { get; set; }

        public UsersPage()
        {
            _userService = Ioc.Resolve<IUserService>();
            Initialize();

        }

        private async Task Initialize()
        {
            await _userService.GetUsers();
            Users = _userService.Users;
            InitializeComponent();
            BindingContext = this;
            userList.ItemsSource = Users;
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
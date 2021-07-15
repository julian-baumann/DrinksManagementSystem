using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core;
using Database.Entities;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.Storage;
using DrinksManagementSystem.Services.User;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserPage : ContentPage
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;

        private FileBase _imageFile;

        public User User { get; set; } = new User();

        public NewUserPage()
        {
            _userService = Ioc.Resolve<IUserService>();
            _storageService = Ioc.Resolve<IStorageService>();

            InitializeComponent();
            BindingContext = this;

        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void AddPhoto(FileBase file)
        {
            if (file == null) return;
            
           _imageFile = file;
        }

        private void OnImageChanged(object sender, FileBase file)
        {
            AddPhoto(file);
        }

        private void OnIsAdminToggled(object sender, ToggledEventArgs e)
        {
            User.Role = e.Value ? UserRoles.Admin : UserRoles.Guest;
        }

        private async void OnAddUserClicked(object sender, EventArgs args)
        {
            if (User.Name == null || User.Name.Length <= 0)
            {
                await DisplayAlert("Fehler", "Bitte nutzer-name eintragen", "Ok");
                return;
            }

            if (_imageFile != null)
            {
                using var stream = await _imageFile.OpenReadAsync();

                var newImagePath = await _storageService.StoreProfilePicture(_imageFile.FileName, stream);
                User.ImagePath = newImagePath;
            }

            User.DateCreated = DateTime.Now;

            var result = await _userService.CreateUser(User);

            if (result >= 0)
            {
                Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok :(");
            }
        }
    }
}
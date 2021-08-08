using System;
using System.Collections.ObjectModel;
using Common.Core;
using DrinksManagementSystem.Entities;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.Storage;
using DrinksManagementSystem.Services.User;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DrinksManagementSystem.Pages
{
    public partial class UserDetailPage : ContentPage
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        private readonly IBoughtDrinkService _boughtDrinkService;

        private FileBase _newImageFile;

        // ReSharper disable once MemberCanBePrivate.Global
        public User User { get; set; }
        public bool IsAdmin { get; set; }

        public UserDetailPage(User user)
        {
            _userService = Ioc.Resolve<IUserService>();
            _storageService = Ioc.Resolve<IStorageService>();
            _boughtDrinkService = Ioc.Resolve<IBoughtDrinkService>();

            User = user.Clone();
            IsAdmin = User.Role == UserRoles.Admin;

            BindingContext = this;
            InitializeComponent();

            unpaidInfoControl.UserId = User.Id;
            editPictureView.ImagePath = User.ImagePath;
        }

        private void ChangeImage(FileBase file)
        {
            if (file == null) return;

            _newImageFile = file;
        }


        private void OnImageChanged(object sender, FileBase file)
        {
            ChangeImage(file);
        }

        private async void OnUpdateClicked(object sender, EventArgs args)
        {
            if (_newImageFile != null)
            {
                using var stream = await _newImageFile.OpenReadAsync();

                if (User.ImagePath != null)
                {
                    await _storageService.RemovePicture(User.ImagePath);
                }

                var newImagePath = await _storageService.StorePicture(_newImageFile.FileName, stream);
                User.ImagePath = newImagePath;
            }

            var result = await _userService.Update(User);

            if (result)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok");
            }
        }


        private async void OnDeleteClicked(object sender, EventArgs args)
        {
            var answer = await DisplayAlert("Löschen", "Bist du dir sicher?", "Ja", "Nein");
            if (!answer) return;

            var result = await _userService.Remove(User);

            if (result)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten", "Ok");
            }
        }

        private void OnIsAdminToggled(object sender, ToggledEventArgs e)
        {
            IsAdmin = e.Value;
            User.Role = e.Value ? UserRoles.Admin : UserRoles.Guest;
        }

        private void OnShowPaidDrinksClicked(System.Object sender, System.EventArgs e)
        {
            var drinks = _boughtDrinkService.GetAllPaidDrinksByUser(User.Id);
            Navigation.PushAsync(new BoughtDrinksOverviewPage(new ObservableCollection<BoughtDrink>(drinks)));
        }
    }
}

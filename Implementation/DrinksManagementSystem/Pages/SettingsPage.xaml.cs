using System;
using Common.Core;
using DrinksManagementSystem.Services.Share;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly IShareService _shareService;

        public SettingsPage()
        {
            _shareService = Ioc.Resolve<IShareService>();

            InitializeComponent();
        }

        private async void OnExportDataClicked(object sender, EventArgs e)
        {
            try
            {
                exportButton.IsEnabled = false;
                exportActivityIndicator.IsVisible = true;
                await _shareService.ShareDatabase();
                exportActivityIndicator.IsVisible = false;
                exportButton.IsEnabled = true;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                await DisplayAlert("Fehler!", "Ein Fehler ist aufgetreten", "OK");
            }
        }

        private async void OnImportDataClicked(object sender, EventArgs e)
        {
            var result = await _shareService.ImportDatabase();

            if (result == null) return;

            if (result == true)
            {
                await DisplayAlert("Erfolg!", "Datenbank und Bilder erfolgreich importiert!", "OK");
            }
            else
            {
                await DisplayAlert("Fehler!", "Ein Fehler ist aufgetreten", "OK");
            }
        }
    }
}
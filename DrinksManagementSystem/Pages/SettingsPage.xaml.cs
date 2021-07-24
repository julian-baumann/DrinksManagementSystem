using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            exportButton.IsEnabled = false;
            exportActivityIndicator.IsVisible = true;
            await _shareService.ShareDatabase();
            exportActivityIndicator.IsVisible = false;
            exportButton.IsEnabled = true;
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
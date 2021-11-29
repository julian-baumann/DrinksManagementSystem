using System;
using System.Threading;
using Common.Core;
using DrinksManagementSystem.Core;
using DrinksManagementSystem.Services.Share;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly IShareService _shareService;
        private bool _exportLoading;

        public bool FlatActivated
        {
            get => AppCore.Flat;
            set => AppCore.Flat = value;
        }

        public bool ExportLoading
        {
            get => _exportLoading;
            set
            {
                _exportLoading = value;
                OnPropertyChanged();
            }
        }

        public SettingsPage()
        {
            _shareService = Ioc.Resolve<IShareService>();

            BindingContext = this;
            InitializeComponent();
        }

        private async void OnExportDataClicked(object sender, EventArgs e)
        {
            try
            {
                ExportButton.IsEnabled = false;
                ExportLoading = true;
                await _shareService.ShareDatabase();
                ExportLoading = false;
                ExportButton.IsEnabled = true;
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
                await DisplayAlert("Datenbank und Bilder erfolgreich importiert!", "Bitte App neu starten", "OK");
            }
            else
            {
                await DisplayAlert("Fehler!", "Ein Fehler ist aufgetreten", "OK");
            }
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void OnOpenEvaluationClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EvaluationInputPage());
        }
    }
}
using System;
using System.Globalization;
using Common.Core;
using DrinksManagementSystem.Services.BoughtDrink;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluationInputPage : ContentPage
    {
        private readonly IBoughtDrinkService _boughtDrinkService;
        private bool _loading;
        private double _flatPrice;

        public string FlatPrice
        {
            get => GetPrice(_flatPrice);
            set => _flatPrice = SetPrice(value);
        }

        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        public bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                OnPropertyChanged();
            }
        }

        public EvaluationInputPage()
        {
            _boughtDrinkService = Ioc.Resolve<IBoughtDrinkService>();
            InitializeComponent();
        }

        private static string GetPrice(double? value)
        {
            if (value == null) return null;

            return value.ToString().Replace(".", ",");
        }

        private static double SetPrice(string value)
        {
            if (value is not {Length: > 0}) { return 0; }

            try
            {
                return !value.Contains(",") ? double.Parse(value) : double.Parse(value, new NumberFormatInfo() { NumberDecimalSeparator = "," });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        private void OnGenerateClicked(object sender, EventArgs e)
        {
            var drinks = _boughtDrinkService.GetAll(StartDate, EndDate, true);

            var page = new EvaluationResultPage(drinks, _flatPrice);
            Navigation.PushAsync(page);
        }
    }
}
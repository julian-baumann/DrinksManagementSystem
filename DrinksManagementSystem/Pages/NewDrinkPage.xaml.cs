using System;
using System.Collections.Generic;
using DrinksManagementSystem.Entities;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDrinkPage : ContentPage
    {
        private FileBase _imageFile;

        public Drink Drink { get; set; } = new Drink();

        public IList<string> DrinkTypes { get; set; } = new List<string>()
        {
            "Bier",
            "Wein",
            "Mischgetränke",
            "Schnaps",
            "Antialkohol",
            "Anderes"
        };

        public NewDrinkPage()
        {
            InitializeComponent();
            BindingContext = this;
        }


        private void OnAddDrinkClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnImageChanged(object sender, FileBase file)
        {
            _imageFile = file;
        }
    }
}
using DrinksManagementSystem.Core;
using Xamarin.Forms;

namespace DrinksManagementSystem
{
    public partial class App : Application
    {
        public App()
        {
            AppCore.Initialize();
            InitializeComponent();

            MainPage = new TabsPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

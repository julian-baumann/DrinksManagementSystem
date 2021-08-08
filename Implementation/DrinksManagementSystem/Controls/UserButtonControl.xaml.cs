using System;
using DrinksManagementSystem.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserButtonControl : ContentView
    {
        public static readonly BindableProperty UserProperty = BindableProperty.Create(
            propertyName: nameof(User),
            returnType: typeof(User),
            declaringType: typeof(UserButtonControl),
            defaultValue: new User(),
            defaultBindingMode: BindingMode.TwoWay
        );

        public User User
        {
            get => (User)GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }

        public event EventHandler<User> Clicked = delegate { };

        public UserButtonControl()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            Clicked.Invoke(this, User);
            await outerFrame.ScaleTo(0.8, 100);
            await outerFrame.ScaleTo(1, 100);
        }
    }
}
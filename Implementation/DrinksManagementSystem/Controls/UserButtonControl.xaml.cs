using System;
using System.Windows.Input;
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

        public ICommand Tapped { get; set; }

        public event EventHandler<User> Clicked = delegate { };

        public UserButtonControl()
        {
            Tapped = new Command(() => OnClicked(null, null), () => IsEnabled);
            InitializeComponent();
            BindingContext = this;
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Clicked.Invoke(this, User);
        }
    }
}
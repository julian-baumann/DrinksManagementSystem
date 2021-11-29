using System;
using System.Windows.Input;
using DrinksManagementSystem.Controls.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamEffects;

namespace DrinksManagementSystem.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomButton : ContentView
    {
        private ButtonTypes _type = ButtonTypes.Filled;
        private bool _loading = false;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(CustomButton),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay
        );

        public static readonly BindableProperty LoadingProperty = BindableProperty.Create(
            propertyName: nameof(Loading),
            returnType: typeof(bool),
            declaringType: typeof(CustomButton),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnLoadingPropertyChanged
        );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ButtonTypes Type
        {
            get => _type;
            set => SetButtonStyle(value);
        }

        public bool Loading
        {
            get => (bool) GetValue(LoadingProperty);
            set
            {
                SetValue(LoadingProperty, value);
                _loading = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler Clicked = delegate { };

        public string ButtonColor { get; set; } = "#3478f6";

        public ICommand TapCommand { get; set; }

        public CustomButton()
        {
            TapCommand = new Command(
                execute: () => OnClicked(null, null),
                canExecute: () => IsEnabled);

            BindingContext = this;
            InitializeComponent();
        }

        private static void OnLoadingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var isLoading = (bool)newValue;
            var control = (CustomButton) bindable;
            control.Loading = isLoading;
        }

        private void SetButtonStyle(ButtonTypes value)
        {
            _type = value;

            var userColor = ButtonColor.Remove(0, 1);

            var backgroundColorLight = "";
            var backgroundColorDark = "";
            var color = "white";

            switch (Type)
            {
                case ButtonTypes.Filled:
                    backgroundColorLight = $"#{userColor}";
                    backgroundColorDark = $"#{userColor}";
                    color = "#ffffff";
                    break;

                case ButtonTypes.Tinted:
                    backgroundColorLight = $"#4D{userColor}";
                    backgroundColorDark = $"#4D{userColor}";
                    color = $"#{userColor}";
                    break;

                case ButtonTypes.Gray:
                    backgroundColorLight = "#e9e9eb";
                    backgroundColorDark = "#131314";
                    color = $"#{userColor}";
                    break;
            }

            CustomButtonBox.SetAppThemeColor(BackgroundColorProperty, Color.FromHex(backgroundColorLight), Color.FromHex(backgroundColorDark));
            CustomButtonLabel.TextColor = Color.FromHex(color);
        }

        private void OnClicked(object sender, EventArgs e)
        {
            if (!Loading)
            {
                Clicked?.Invoke(sender, e);
            }
        }
    }
}
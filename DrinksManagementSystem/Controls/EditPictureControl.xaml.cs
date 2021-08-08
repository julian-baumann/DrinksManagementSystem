using System;
using System.IO;
using DrinksManagementSystem.Core;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPictureControl : ContentView
    {
        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (value == null) return;

                var fullPath = Path.Combine(AppCore.StoragePath, value);
                _imagePath = fullPath;

                if (fileImageSourceElement != null)
                {
                    fileImageSourceElement.File = fullPath;
                }
            }
        }

        public event EventHandler<FileBase> ImageChanged = delegate { };

        public EditPictureControl()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void ChangeImage(FileBase file)
        {
            if (file == null) return;

            var stream = await file.OpenReadAsync();
            profileImage.Source = ImageSource.FromStream(() => stream);
            ImageChanged.Invoke(this, file);
        }

        private async void OnChangeImageClicked(object sender, EventArgs e)
        {
            var selectedImageFile = await MediaPicker.PickPhotoAsync();
            ChangeImage(selectedImageFile);
        }

        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            if (!MediaPicker.IsCaptureSupported) return;

            var selectedImageFile = await MediaPicker.CapturePhotoAsync();
            ChangeImage(selectedImageFile);
        }

    }
}
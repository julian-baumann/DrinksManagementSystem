using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core;
using DrinksManagementSystem.Core;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPictureView : ContentView
    {
        public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(
            propertyName: nameof(ImagePath),
            returnType: typeof(string),
            declaringType: typeof(EditPictureView),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay
            );

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

        public EditPictureView()
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
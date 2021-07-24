using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using DrinksManagementSystem.Droid.Services;
using DrinksManagementSystem.Services.ImageResizer;
using Bitmap = Android.Graphics.Bitmap;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidImageResizer))]
namespace DrinksManagementSystem.Droid.Services
{
    public class AndroidImageResizer : IImageResizerService
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            return ResizeImageAndroid(imageData, width, height);
        }

        public byte[] ResizeImageAndroid (byte[] imageData, float maxWidth, float maxHeight)
        {
            // Load the bitmap
            var originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            Bitmap croppedImage;

            if (originalImage.Width >= originalImage.Height)
            {
                var x = originalImage.Width / 2 - originalImage.Height / 2;
                var y = 0;
                croppedImage = Bitmap.CreateBitmap(originalImage, (int)x , (int)y, (int)originalImage.Height, (int)originalImage.Height);
            }
            else
            {
                var x = 0;
                var y = originalImage.Height / 2 - originalImage.Width / 2;
                croppedImage = Bitmap.CreateBitmap(originalImage, (int)x , (int)y, (int)originalImage.Width, (int)originalImage.Width);
            }

            using var ms = new MemoryStream();
            var resizedImage = Bitmap.CreateScaledBitmap(croppedImage, (int)maxWidth, (int)maxHeight, false);


            resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 40, ms);
            return ms.ToArray();
        }
    }
}
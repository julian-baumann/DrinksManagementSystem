using System;
using System.Drawing;
using CoreGraphics;
using DrinksManagementSystem.iOS.Services;
using DrinksManagementSystem.Services.ImageResizer;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSImageResizer))]
namespace DrinksManagementSystem.iOS.Services
{
    public class iOSImageResizer : IImageResizerService
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            return ResizeImageIOS(imageData, width, height);
        }

        private UIImage Crop(UIImage image, int x, int y, int width, int height)
        {
            var imgSize = image.Size;

            UIGraphics.BeginImageContext(new SizeF(width, height));
            var imgToCrop = UIGraphics.GetCurrentContext();

            var croppingRectangle = new RectangleF(0, 0, width, height);
            imgToCrop.ClipToRect(croppingRectangle);

            var drawRectangle = new RectangleF(-x, -y, (float)imgSize.Width, (float)imgSize.Height);

            image.Draw(drawRectangle);
            var croppedImg = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();
            return croppedImg;
        }

        private byte[] ResizeImageIOS(byte[] imageData, float width, float height)
        {
            var originalImage = ImageFromByteArray(imageData);
            var orientation = originalImage.Orientation;

            UIImage croppedImage;

            if (originalImage.Size.Width >= originalImage.Size.Height)
            {
                var x = originalImage.Size.Width / 2 - originalImage.Size.Height / 2;
                var y = 0;
                croppedImage = Crop(originalImage, (int)x , (int)y, (int)originalImage.Size.Height, (int)originalImage.Size.Height);
            }
            else
            {
                var x = 0;
                var y = originalImage.Size.Height / 2 - originalImage.Size.Width / 2;
                croppedImage = Crop(originalImage, (int)x , (int)y, (int)originalImage.Size.Width, (int)originalImage.Size.Width);
            }

            //create a 24bit RGB image
            using (var context = new CGBitmapContext(IntPtr.Zero,
                (int)width, (int)height, 8,
                4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                CGImageAlphaInfo.PremultipliedFirst))
            {

                var imageRect = new RectangleF(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, croppedImage.CGImage);

                var resizedImage = UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        private UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIImage image;
            try
            {
                image = new UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}
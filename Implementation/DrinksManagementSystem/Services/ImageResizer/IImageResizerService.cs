namespace DrinksManagementSystem.Services.ImageResizer
{
    public interface IImageResizerService
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
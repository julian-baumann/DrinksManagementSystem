
using DrinksManagementSystem;
using DrinksManagementSystem.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabsPage), typeof(NativeTabBarRenderer))]
namespace DrinksManagementSystem.iOS.Renderers
{
    public class NativeTabBarRenderer : TabbedRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TabBar.Translucent = true;
            // EdgesForExtendedLayout = UIRectEdge.Bottom;
            // ViewController.EdgesForExtendedLayout = UIRectEdge.Bottom;
            // ViewController.ExtendedLayoutIncludesOpaqueBars = true;
        }
    }
}
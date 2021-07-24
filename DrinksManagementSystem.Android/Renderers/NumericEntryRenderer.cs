

using DrinksManagementSystem.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(NumericEntryRenderer))]
namespace DrinksManagementSystem.Droid.Renderers
{
    public class NumericEntryRenderer : EntryRenderer
    {

        public NumericEntryRenderer(Android.Content.Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;

            if (Element.Keyboard != Keyboard.Numeric) return;

            Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
            Control.KeyListener = Android.Text.Method.DigitsKeyListener.GetInstance("1234567890,");

        }

    }
}
using System;
using System.Globalization;
using DrinksManagementSystem.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Converter
{
    public class DrinkPriceConverter : IValueConverter, IMarkupExtension
    {
        private string ConvertPrice(double? price)
        {
            return ((double) price).ToString("F").Replace(".", ",") + " €";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var drink = (Drink) value;
            var user = (User) parameter;

            if (user == null || drink == null) return "";

            if (user.Role == UserRoles.Guest)
            {
                return ConvertPrice(drink.Price);
            }
            else if (user.Role == UserRoles.Guest && drink.AdminPrice is > 0)
            {
                return ConvertPrice(drink.AdminPrice);
            }
            else if (user.Role == UserRoles.Admin && drink.AdminPrice == null ||
                     drink.AdminPrice == 0 && drink.Price is > 0)
            {
                return ConvertPrice(drink.Price);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Common.Core;
using DrinksManagementSystem.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinksManagementSystem.Converter
{
    public class BrandsConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            Logger.Info(string.Join(", ", value as ObservableCollection<DrinkBrand>));

            return string.Join(", ", value as ObservableCollection<DrinkBrand> ?? new ());
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
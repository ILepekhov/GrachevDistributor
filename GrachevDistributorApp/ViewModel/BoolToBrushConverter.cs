using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GrachevDistributorApp.ViewModel
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value
                ? Brushes.Green
                : Brushes.Firebrick;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

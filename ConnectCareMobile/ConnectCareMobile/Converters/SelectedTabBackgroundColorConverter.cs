using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ConnectCareMobile.Converters
{
    public class SelectedTabBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Color.Transparent;
            if (value is bool Value)
            {
                if (Value)
                {
                    return Color.FromHex("#0036BF");
                }
                else return Color.Transparent;
            }
            else return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Transparent;
        }
    }
}

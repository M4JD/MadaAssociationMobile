using System;
using System.Globalization;
using Xamarin.Forms;

namespace ConnectCareMobile.Converters
{
    public class SelectedTabIconColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Color.FromHex("#D0D0D5");
            if (value is bool Value)
            {
                if (Value)
                {
                    return Color.White;
                }
                else return Color.FromHex("#D0D0D5");
            }
            else return Color.FromHex("#D0D0D5");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromHex("#D0D0D5");
        }
    }
}

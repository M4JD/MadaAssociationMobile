using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ConnectCareMobile.Converters
{
    public class MessageFrameBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessagesResponse Value)
            {
                if (Value != null)
                {
                    if (Value.UserId == GlobalSettings.LoggedUser.UserId)
                    {
                        // Current user is the sender
                        return Color.FromHex("#2965FF");
                    }
                    else
                    {
                        // Current user is the reciever
                        return Color.FromHex("#2965FF");
                    }
                }
                else return Color.Transparent;
            }
            else
                return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Transparent;
        }
    }
}

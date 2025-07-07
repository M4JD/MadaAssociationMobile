using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MadaAssociationMobile.Converters
{
    public class MessageTextConverter : IValueConverter
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
                        return Color.White;
                    }
                    else {
                        // Current user is the reciever
                        return Color.Black;
                    }
                }
                else return Color.Black;
            }
            else
                return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Black;
        }
    }
}

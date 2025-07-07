using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MadaAssociationMobile.Converters
{
    public class MessageFrameMarginConverter : IValueConverter
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
                        return new Thickness(30, 0, 0, 0);
                    }
                    else
                    {
                        // Current user is the reciever
                        return new Thickness(0, 0, 30, 0);
                    }
                }
                else return new Thickness(0);
            }
            else
                return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Thickness(0);
        }
    }
}

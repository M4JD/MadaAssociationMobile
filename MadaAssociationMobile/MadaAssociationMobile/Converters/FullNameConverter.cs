using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MadaAssociationMobile.Converters
{
    public class FullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (value is MemberResponse Value)
            {
                if (Value != null)
                {
                    return $"{Value.FirstName} {Value.LastName}";
                }
                else return string.Empty;
            }
            else
            {
                if (value is ChatResponse ChatValue)
                {
                    if (ChatValue != null)
                    {
                        var fullname = string.Empty;
                        if (GlobalSettings.LoggedUser.UserId == ChatValue.ReceiverId)
                        {
                            fullname = $"{ChatValue.Sender.FirstName} {ChatValue.Sender.LastName}";
                        }
                        else
                        { 
                            fullname = $"{ChatValue.Receiver.FirstName} {ChatValue.Receiver.LastName}";
                        }
                        return fullname;
                    }
                    else return string.Empty;
                }
                else
                {
                    return string.Empty;
                }

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}

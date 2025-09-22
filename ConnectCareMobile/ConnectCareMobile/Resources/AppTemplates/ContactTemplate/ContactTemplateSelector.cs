using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ConnectCareMobile.Resources
{
    public class ContactTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PendingContactTemplate { get; set; }
        public DataTemplate AcceptedContactTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is MemberResponse value)
            {
                if (value.IsAccepted)
                {
                    return AcceptedContactTemplate;
                }
                else
                {
                    return PendingContactTemplate;
                }
            }
            else
            {
                return PendingContactTemplate;
            }
        }
    }
}

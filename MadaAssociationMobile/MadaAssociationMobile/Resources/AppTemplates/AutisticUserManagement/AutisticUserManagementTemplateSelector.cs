using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MadaAssociationMobile.Resources
{
    internal class AutisticUserManagementTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PendingUserTemplate { get; set; }
        public DataTemplate AcceptedUserTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is MemberResponse value)
            {
                if (value.IsApproved)
                {
                    return AcceptedUserTemplate;
                }
                else
                {
                    return PendingUserTemplate;
                }
            }
            else
            {
                return PendingUserTemplate;
            }
        }
    }
}

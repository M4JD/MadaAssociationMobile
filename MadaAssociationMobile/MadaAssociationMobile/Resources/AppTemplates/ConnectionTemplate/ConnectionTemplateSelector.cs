using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MadaAssociationMobile.Resources
{
    public class ConnectionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PendingApprovalTemplate { get; set; }
        public DataTemplate ApprovedTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is MemberResponse value)
            {
                if (value.IsApproved)
                {
                    return ApprovedTemplate;
                }
                else
                {
                    return PendingApprovalTemplate;
                }
            }
            else
            {
                return PendingApprovalTemplate;
            }
        }
    }
}

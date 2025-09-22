using ConnectCareMobile.Helpers;
using ConnectCareMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ConnectCareMobile.Resources
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate InfoTemplate { get; set; }
        public DataTemplate FileTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }
        public DataTemplate AudioTemplate { get; set; }
        public DataTemplate VerificationTemplate { get; set; }
        public DataTemplate MessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is MessagesResponse value)
            {
                switch (value.MessageType)
                {
                    case "Info":
                        return InfoTemplate;
                    case "Message":
                        return MessageTemplate;
                    case "File":
                        return FileTemplate;
                    case "Image":
                        return ImageTemplate;
                    case "Audio":
                        return AudioTemplate;
                    case "Verification":
                        return VerificationTemplate;
                    default: return MessageTemplate;
                }
            }
            else
            {
                return MessageTemplate;
            }
        }
    }
}

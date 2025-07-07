using Foundation;
using MadaAssociationMobile.Controls;
using MadaAssociationMobile.CustomRenderers;
using MadaAssociationMobile.iOS.CustomRenderers;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomFrame), typeof(EntryRenderer))]
namespace MadaAssociationMobile.iOS.CustomRenderers
{
    internal class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            TintCustomization(Control, e.NewElement as CustomEntry);
        }

        private void TintCustomization(UITextField Control, CustomEntry customEntry)
        {
            if (Control == null) return;

            if (customEntry != null)
            {
                UITextField textField = Control;
                textField.BorderStyle = UITextBorderStyle.None;
                textField.TintColor = customEntry.HandleColor.ToUIColor();
            }
        }
    }
}
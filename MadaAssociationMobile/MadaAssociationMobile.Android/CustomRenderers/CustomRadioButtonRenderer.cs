using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MadaAssociationMobile.Controls;
using MadaAssociationMobile.CustomRenderers;
using MadaAssociationMobile.Droid.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomRadioButton), typeof(CustomRadioButtonRenderer))]
namespace MadaAssociationMobile.Droid.CustomRenderers
{
    public class CustomRadioButtonRenderer : RadioButtonRenderer
    {
        public CustomRadioButtonRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.RadioButton> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (e.NewElement is CustomRadioButton customRadio)
                {
                    Control.ButtonTintList = ColorStateList.ValueOf(Android.Graphics.Color.ParseColor(customRadio.HighLightColor));
                    Control.SetHighlightColor(Android.Graphics.Color.ParseColor(customRadio.HighLightColor));
                }
            }
        }
    }
}
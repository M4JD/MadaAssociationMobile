using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MadaAssociationMobile.Droid.Effects;
using MadaAssociationMobile.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using static MadaAssociationMobile.Effects.TintImageColor;

[assembly: Xamarin.Forms.ResolutionGroupName("MAAN")]
[assembly: Xamarin.Forms.ExportEffect(typeof(TintImageEffect), nameof(TintImageEffect))]
namespace MadaAssociationMobile.Droid.Effects
{
    public class TintImageEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (TintImage)Element.Effects.FirstOrDefault(e => e is TintImage);

                if (effect == null || !(Control is ImageView image))
                    return;
                Color TintColor = effect.Color.ToAndroid();
                var filter = new PorterDuffColorFilter(TintColor, PorterDuff.Mode.SrcIn);
                image.SetColorFilter(filter);
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnDetached() { }
    }
}
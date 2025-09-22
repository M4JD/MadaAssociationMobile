using ConnectCareMobile.Controls;
using ConnectCareMobile.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ConnectCareMobile.Effects
{
    public static class TintImageColor
    {
        public static readonly BindableProperty ImageColorProperty =
          BindableProperty.CreateAttached("ImageColor", typeof(Color), typeof(TintImageColor), Color.White, propertyChanged: OnColorChanged);
        static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
            {
                return;
            }
            Color ImageTintColor = Color.White;
            if (newValue != oldValue)
                ImageTintColor = (Color)newValue;
            var toRemove = view.Effects.FirstOrDefault(e => e is TintImage);
            if (toRemove != null)
            {
                view.Effects.Remove(toRemove);
            }
            view.Effects.Add(new TintImage(ImageTintColor));
        }
        public class TintImage : RoutingEffect
        {
            public Color Color
            {
                get;
                set;
            }
            public TintImage(Color color) : base($"MAAN.TintImageEffect")
            {
                Color = color;
            }
        }
    }
}

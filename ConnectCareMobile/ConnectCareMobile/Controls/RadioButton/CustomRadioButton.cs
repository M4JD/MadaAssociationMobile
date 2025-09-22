using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ConnectCareMobile.Controls
{
    public class CustomRadioButton : RadioButton
    {
        public static readonly BindableProperty HighLightColorProperty = BindableProperty.Create(nameof(HighLightColor), typeof(string), typeof(CustomRadioButton), defaultValue: "#000000");
        public string HighLightColor
        {
            get => (string)GetValue(HighLightColorProperty);
            set => SetValue(HighLightColorProperty, value);
        }

        //public static readonly BindableProperty OutlineColorProperty = BindableProperty.Create(nameof(OutlineColor), typeof(CornerRadius), typeof(CustomRadioButton), defaultValue: "#000000");
        //public string OutlineColor
        //{
        //    get => (string)GetValue(OutlineColorProperty);
        //    set => SetValue(OutlineColorProperty, value);
        //}
    }
}

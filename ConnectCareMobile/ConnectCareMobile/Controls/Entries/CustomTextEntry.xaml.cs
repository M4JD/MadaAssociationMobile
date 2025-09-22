using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConnectCareMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTextEntry : ContentView
    {
        public CustomTextEntry()
        {
            InitializeComponent();
        }
        //Text Property
        public static BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(CustomTextEntry), string.Empty, defaultBindingMode: BindingMode.TwoWay);
        public string Text
        {
            get { return ((string)GetValue(TextProperty)); }
            set { SetValue(TextProperty, value); }
        }
        //Placeholder Property
        public static BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(CustomTextEntry), string.Empty, defaultBindingMode: BindingMode.TwoWay);
        public string Placeholder
        {
            get { return ((string)GetValue(PlaceholderProperty)); }
            set { SetValue(PlaceholderProperty, value); }
        }
        //Placeholder Title
        public static BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(CustomTextEntry), string.Empty);
        public string Title
        {
            get { return ((string)GetValue(TitleProperty)); }
            set { SetValue(TitleProperty, value); }
        }
        //IsError Property
        public static BindableProperty IsErrorProperty = BindableProperty.Create("IsError", typeof(bool), typeof(CustomTextEntry), false);
        public bool IsError
        {
            get { return ((bool)GetValue(IsErrorProperty)); }
            set { SetValue(IsErrorProperty, value); }
        }
        //ErrorText Property
        public static BindableProperty ErrorTextProperty = BindableProperty.Create("ErrorText", typeof(string), typeof(CustomTextEntry), string.Empty);
        public string ErrorText
        {
            get { return ((string)GetValue(ErrorTextProperty)); }
            set { SetValue(ErrorTextProperty, value); }
        }
        //IsPassword Property
        public static BindableProperty IsPasswordProperty = BindableProperty.Create("IsPassword", typeof(bool), typeof(CustomTextEntry), false);
        public bool IsPassword
        {
            get { return ((bool)GetValue(IsPasswordProperty)); }
            set { SetValue(IsPasswordProperty, value); }
        }

        private void CustomEntry_Focused(object sender, FocusEventArgs e)
        {
            VisualStateManager.GoToState(EntryOuterFrame, "Focused");
            VisualStateManager.GoToState(EntryInnerFrame, "Focused");
            VisualStateManager.GoToState(TitleLabel, "Focused");
        }

        private void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        {
            VisualStateManager.GoToState(TitleLabel, "Normal");
            VisualStateManager.GoToState(EntryOuterFrame, "Normal");
            VisualStateManager.GoToState(EntryInnerFrame, "Normal");
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label showHide = (Label)sender;
            showHide.Text = CustomEntryControl.IsPassword ? "Hide" : "Show";
            CustomEntryControl.IsPassword = !CustomEntryControl.IsPassword;
        }
    }
}
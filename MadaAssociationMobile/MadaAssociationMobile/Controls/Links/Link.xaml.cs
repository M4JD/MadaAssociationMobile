using MadaAssociationMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadaAssociationMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Link : ContentView
    {
        public Link()
        {
            //HorizontalOptions = LayoutOptions.FillAndExpand;
            InitializeComponent();
        }
        //Text Property
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Link), string.Empty);
        public string Text
        {
            get { return ((string)GetValue(TextProperty)); }
            set { SetValue(TextProperty, value); }
        }
        //Command Property
        public static BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(CommandAsync), typeof(Link), new CommandAsync(async () => { }, () => false));
        public CommandAsync Command
        {
            get { return ((CommandAsync)GetValue(CommandProperty)); }
            set { SetValue(CommandProperty, value); }
        }
        //CommandParameter Property
        public static BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(Link), null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        ////HorizontalOptions Property
        //public new static BindableProperty HorizontalOptionsProperty = BindableProperty.Create(nameof(HorizontalOptions), typeof(LayoutOptions), typeof(Link), LayoutOptions.FillAndExpand);
        //public new object HorizontalOptions
        //{
        //    get { return GetValue(HorizontalOptionsProperty); }
        //    set { SetValue(HorizontalOptionsProperty, value); }
        //}
        ////VerticalOptions Property
        //public new static BindableProperty VerticalOptionsProperty = BindableProperty.Create(nameof(VerticalOptions), typeof(LayoutOptions), typeof(Link), LayoutOptions.FillAndExpand);
        //public new object VerticalOptions
        //{
        //    get { return GetValue(VerticalOptionsProperty); }
        //    set { SetValue(VerticalOptionsProperty, value); }
        //}
    }
}
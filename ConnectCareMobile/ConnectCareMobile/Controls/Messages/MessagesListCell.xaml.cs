using ConnectCareMobile.ViewModels.Base;
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
    public partial class MessagesListCell : ContentView
    {
        public MessagesListCell()
        {
            InitializeComponent();
        }

        //LastMessage Property
        public static BindableProperty LastMessageProperty = BindableProperty.Create(nameof(LastMessage), typeof(string), typeof(MessagesListCell), string.Empty);
        public string LastMessage
        {
            get { return ((string)GetValue(LastMessageProperty)); }
            set { SetValue(LastMessageProperty, value); }
        }
        //MessageCount Property
        public static BindableProperty MessageCountProperty = BindableProperty.Create(nameof(MessageCount), typeof(int), typeof(MessagesListCell), 0);
        public int MessageCount
        {
            get { return ((int)GetValue(MessageCountProperty)); }
            set { SetValue(MessageCountProperty, value); }
        }
        //ContactAvatar Property
        public static BindableProperty ContactAvatarProperty = BindableProperty.Create(nameof(ContactAvatar), typeof(string), typeof(MessagesListCell), string.Empty);
        public string ContactAvatar
        {
            get { return ((string)GetValue(ContactAvatarProperty)); }
            set { SetValue(ContactAvatarProperty, value); }
        }
        //ContactName Property
        public static BindableProperty ContactNameProperty = BindableProperty.Create(nameof(ContactName), typeof(string), typeof(MessagesListCell), string.Empty);
        public string ContactName
        {
            get { return ((string)GetValue(ContactNameProperty)); }
            set { SetValue(ContactNameProperty, value); }
        }
        //LastMessageTime Property
        public static BindableProperty LastMessageTimeProperty = BindableProperty.Create(nameof(LastMessageTime), typeof(string), typeof(MessagesListCell), string.Empty);
        public string LastMessageTime
        {
            get { return ((string)GetValue(LastMessageTimeProperty)); }
            set { SetValue(LastMessageTimeProperty, value); }
        }
        //ClickCommand Property
        public static BindableProperty ClickCommandProperty = BindableProperty.Create(nameof(ClickCommand), typeof(CommandAsync<object>), typeof(MessagesListCell), null);
        public CommandAsync<object> ClickCommand
        {
            get { return ((CommandAsync<object>)GetValue(ClickCommandProperty)); }
            set { SetValue(ClickCommandProperty, value); }
        }
        //CommandParameter Property
        public static BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MessagesListCell), null);
        public object CommandParameter
        {
            get { return (GetValue(CommandParameterProperty)); }
            set { SetValue(CommandParameterProperty, value); }
        }
    }
}
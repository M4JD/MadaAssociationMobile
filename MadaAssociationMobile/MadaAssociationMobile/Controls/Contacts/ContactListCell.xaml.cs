using MadaAssociationMobile.Controls;
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
    public partial class ContactListCell : ContentView
    {
        public ContactListCell()
        {
            InitializeComponent();
        }
        //ContactAvatar Property
        public static BindableProperty ContactAvatarProperty = BindableProperty.Create(nameof(ContactAvatar), typeof(string), typeof(ContactListCell), string.Empty);
        public string ContactAvatar
        {
            get { return ((string)GetValue(ContactAvatarProperty)); }
            set { SetValue(ContactAvatarProperty, value); }
        }
        //ContactName Property
        public static BindableProperty ContactNameProperty = BindableProperty.Create(nameof(ContactName), typeof(string), typeof(ContactListCell), string.Empty);
        public string ContactName
        {
            get { return ((string)GetValue(ContactNameProperty)); }
            set { SetValue(ContactNameProperty, value); }
        }
        ///Command Property
        public static BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(CommandAsync<object>), typeof(ContactListCell), new CommandAsync<object>(async (data) => { }, (data) => false));
        public CommandAsync<object> Command
        {
            get { return ((CommandAsync<object>)GetValue(CommandProperty)); }
            set { SetValue(CommandProperty, value); }
        }
        //CommandParameter Property
        public static BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ContactListCell), null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
    }
}
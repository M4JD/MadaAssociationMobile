using MadaAssociationMobile.Common.Global.Enum;
using MadaAssociationMobile.Controls;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.ViewModels;
using MadaAssociationMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadaAssociationMobile.Views.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseView : ContentPage
    {
        public BaseView()
        {
            InitializeComponent();
        }
        public static BindableProperty StatusBarProperty = BindableProperty.Create(nameof(StatusBar), typeof(StatusBar), typeof(BaseView), new StatusBar(), propertyChanged: OnStatusBarChanged, defaultBindingMode: BindingMode.TwoWay);
        public StatusBar StatusBar
        {
            get { return ((StatusBar)GetValue(StatusBarProperty)); }
            set { SetValue(StatusBarProperty, value); }
        }
        private static void OnStatusBarChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var device = DependencyService.Get<IDeviceInfo>();
            StatusBar NewValue = (StatusBar)newValue;
            if (newValue != oldValue)
                (bindable as BaseView).StatusBar = NewValue;
            device?.SetStatusBarColor(NewValue.StatusBarColor, NewValue.DarkStatusBarTint);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //NavigationService.SetCurrectPage(this);
            var bindingContext = BindingContext as BaseViewModel;
            if (bindingContext != null)
            {
                bindingContext.OnAppearing(this);
            }
        }
        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;

            if (bindingContext != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await bindingContext.BackButtonCommand?.ExecuteAsync();
                });
            }
            return true;
        }
        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<object, string>(Application.Current.MainPage, MessagingCenterTokenEnum.Language.ToString());
            base.OnDisappearing();

            var bindingContext = BindingContext as BaseViewModel;

            if (bindingContext != null)
            {
                bindingContext.OnDisappearing();
            }
        }
    }
}
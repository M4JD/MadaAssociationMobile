using Autofac;
using MadaAssociationMobile.APIServices;
using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Helpers;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.NavigationServices;
using MadaAssociationMobile.Views;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace MadaAssociationMobile.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer _container;
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);
        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }
        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Services
            builder.RegisterType<Settings>().As<ISettings>().SingleInstance();
            builder.RegisterType<ConnectionsAPIService>().As<IConnectionsAPIService>().SingleInstance();
            builder.RegisterType<MessagesAPIService>().As<IMessagesAPIService>().SingleInstance();
            builder.RegisterType<ChatAPIService>().As<IChatAPIService>().SingleInstance();
            builder.RegisterType<UserAPIService>().As<IUserAPIService>().SingleInstance();
            builder.RegisterType<UserManagementAPIService>().As<IUserManagementAPIService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            DependencyService.Register<IDeviceInfo>();
            DependencyService.Register<IFirebaseMessagingHelper>();
            DependencyService.Register<IAudioRecorder>();
            DependencyService.Register<ISinchService>();

            // View models
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<UserFriendsViewModel>();
            builder.RegisterType<UserProfileViewModel>();
            builder.RegisterType<UserProfileManagementViewModel>();
            builder.RegisterType<UserManagementViewModel>();
            builder.RegisterType<ComingSoonViewModel>();
            builder.RegisterType<ChatDetailsModel>();
            builder.RegisterType<VerifyPhoneNumberViewModel>();
            builder.RegisterType<AccountTypeViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<AccountInfoViewModel>();
            builder.RegisterType<CareTakerInfoViewModel>();
            builder.RegisterType<PersonalInfoViewModel>();
            builder.RegisterType<LinkCareTakerViewModel>();
            builder.RegisterType<ForgetPasswordViewModel>();
            builder.RegisterType<MessagesListViewModel>();
            builder.RegisterType<ContactsViewModel>();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<AboutViewModel>();

            // Models
            builder.RegisterType<UserFriendsModel>();
            builder.RegisterType<UserProfileModel>();
            builder.RegisterType<UserProfileManagementModel>();
            builder.RegisterType<UserManagementModel>();
            builder.RegisterType<ChatDetailsViewModel>();
            builder.RegisterType<VerifyPhoneNumberModel>();
            builder.RegisterType<AccountTypeModel>();
            builder.RegisterType<LoginModel>();
            builder.RegisterType<ProfileModel>();
            builder.RegisterType<PersonalInfoModel>();
            builder.RegisterType<AccountInfoModel>();
            builder.RegisterType<CareTakerInfoModel>();
            builder.RegisterType<LinkCareTakerModel>();
            builder.RegisterType<ForgetPasswordModel>();
            builder.RegisterType<MessagesListModel>();
            builder.RegisterType<ContactsModel>();
            builder.RegisterType<SettingsModel>();
            builder.RegisterType<AboutModel>();

            if (_container != null)
            {
                _container.Dispose();
            }
            _container = builder.Build();
        }
        public static T Resolve<T>()
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
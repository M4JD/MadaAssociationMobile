using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.ViewModels.Base;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MadaAssociationMobile.Common.Global;

namespace MadaAssociationMobile
{
    public partial class App : Application
    {
        public INavigationService NavigationService = null;
        public App()
        {
            InitializeComponent();
            InitNavigation();
        }
        private Task InitNavigation()
        {
            try
            {
                App.Current.UserAppTheme = OSAppTheme.Light;
                ViewModelLocator.RegisterDependencies();
                NavigationService = ViewModelLocator.Resolve<INavigationService>();
                return NavigationService.InitializeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void OnStart()
        {
            AppCenter.Start($"android={GlobalSettings.AndroidAppCenterSecret};" +
                  $"ios={GlobalSettings.IOSAppCenterSecret};",
                  typeof(Analytics), typeof(Crashes));

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.ViewModels.Base;
using ConnectCareMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        public SettingsViewModel(INavigationService navigationService)
        {
            var color = ColorConverters.FromHex("#2965FF");
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            PageTitle = "Settings";
            HasBackButton = AppSharedPrefences.Instance.IsAutistic;
            HasNavBarOptionButton = true;
            StatusBar = new Base.StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            HasTabBar = !AppSharedPrefences.Instance.IsAutistic;
            MenuItems = new ObservableCollection<Base.MenuItem>() { new Base.MenuItem() {
                Text = "About",
                Command = AboutCommand
            }};
            this.navigationService = navigationService;
        }
        #region Properties
        private string _UserFullName = string.Empty;
        public string UserFullName
        {
            get { return _UserFullName; }
            set
            {
                _UserFullName = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public override Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                UserFullName = $"{GlobalSettings.LoggedUser.FirstName} {GlobalSettings.LoggedUser.LastName}";
                navigationService.UpdateBarBackgroundColor("#2965FF");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands

        private CommandAsync _ChangeProfileCommand;
        public CommandAsync ChangeProfileCommand
        {
            get
            {

                return _ChangeProfileCommand ??
                    (_ChangeProfileCommand = new CommandAsync(async () =>
                    {
                        //await navigationService.NavigateTo<ProfileViewModel>(null, false);
                    }));
            }
        }
        private CommandAsync _AboutCommand;
        public CommandAsync AboutCommand
        {
            get
            {

                return _AboutCommand ??
                    (_AboutCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            await HideInflatorMenuCommand.ExecuteAsync();
                            await navigationService.NavigateTo<AboutViewModel>(null, false);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }));
            }
        }
        private CommandAsync _ViewProfileCommand;
        public CommandAsync ViewProfileCommand
        {
            get
            {

                return _ViewProfileCommand ??
                    (_ViewProfileCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            await navigationService.NavigateTo<ProfileViewModel>(null, false);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }));
            }
        }
        private CommandAsync _ChangeLanguageCommand;
        public CommandAsync ChangeLanguageCommand
        {
            get
            {

                return _ChangeLanguageCommand ??
                    (_ChangeLanguageCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            IsActionSheetActive = true;
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                            Console.WriteLine(ex.Message);
                        }
                    }));
            }
        }
        private CommandAsync _LogoutCommand;
        public CommandAsync LogoutCommand
        {
            get
            {

                return _LogoutCommand ??
                    (_LogoutCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            AppSharedPrefences.ResetLogoutSharedPreferences();
                            await navigationService.NavigateToRoot();
                            await navigationService.NavigateTo<LoginViewModel>(null, true);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }));
            }
        }
        #endregion

    }
}

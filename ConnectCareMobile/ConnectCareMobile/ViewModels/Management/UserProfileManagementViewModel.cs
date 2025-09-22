using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    public class UserProfileManagementViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        public UserProfileManagementViewModel(INavigationService navigationService)
        {
            var color = ColorConverters.FromHex("#2965FF");
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            PageTitle = "Autistic User Management";
            HasBackButton = true;
            HasNavBarOptionButton = false;
            StatusBar = new StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            HasTabBar = false;
            this.navigationService = navigationService;
        }
        #region Properties
        private MemberResponse _User = null;
        public MemberResponse User
        {
            get { return _User; }
            set
            {
                _User = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public override Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                if (navigationData != null && navigationData.ContainsKey("MemberInfo"))
                {
                    User = navigationData["MemberInfo"] as MemberResponse;
                }
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

        private CommandAsync _ViewFriendsCommand;
        public CommandAsync ViewFriendsCommand
        {
            get
            {

                return _ViewFriendsCommand ??
                    (_ViewFriendsCommand = new CommandAsync(async () =>
                    {
                        Dictionary<string, object> pageparams = new Dictionary<string, object>();
                        pageparams.Add("MemberInfo", User);
                        await navigationService.NavigateTo<UserFriendsViewModel>(pageparams);
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
                            Dictionary<string, object> pageparams = new Dictionary<string, object>();
                            pageparams.Add("MemberInfo", User);
                            await navigationService.NavigateTo<UserProfileViewModel>(pageparams);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }));
            }
        }
        private CommandAsync _ViewCaregiversCommand;
        public CommandAsync ViewCaregiversCommand
        {
            get
            {

                return _ViewCaregiversCommand ??
                    (_ViewCaregiversCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            Dictionary<string, object> pageparams = new Dictionary<string, object>();
                            pageparams.Add("Title", "User Management (Caregivers)");
                            await navigationService.NavigateTo<ComingSoonViewModel>(pageparams);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }));
            }
        }
        private CommandAsync _ViewUserPreferencesCommand;
        public CommandAsync ViewUserPreferencesCommand
        {
            get
            {

                return _ViewUserPreferencesCommand ??
                    (_ViewUserPreferencesCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            Dictionary<string, object> pageparams = new Dictionary<string, object>();
                            pageparams.Add("Title", "User Management (Preferences)");
                            await navigationService.NavigateTo<ComingSoonViewModel>(pageparams);
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

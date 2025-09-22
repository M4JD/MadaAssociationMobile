using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace ConnectCareMobile.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        public UserProfileViewModel(INavigationService navigationService)
        {
            var color = ColorConverters.FromHex("#2965FF");
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            PageTitle = "Autistic User Management (Profile)";
            HasBackButton = true;
            HasNavBarOptionButton = false;
            StatusBar = new StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            HasTabBar = false;
            this.navigationService = navigationService;
        }

        #region Properties
        private string _Phone = string.Empty;
        public string Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                RaisePropertyChanged();
            }
        }
        private string _Username = string.Empty;
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                RaisePropertyChanged();
            }
        }
        private string _Country = string.Empty;
        public string Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                RaisePropertyChanged();
            }
        }
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
                    Phone = $"{User.PhoneNumericCountryCode}{User.Phone}";
                    GlobalSettings.GetCountryList();
                    Country = GlobalSettings.CountryList.Where(country => country.CountryCode == User.Country).FirstOrDefault()?.Name;
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
                            //await navigationService.NavigateTo<ProfileViewModel>(pageparams);
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

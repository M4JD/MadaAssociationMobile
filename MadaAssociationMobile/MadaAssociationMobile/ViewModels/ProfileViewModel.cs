using MadaAssociationMobile.Common.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MadaAssociationMobile.ViewModels
{
    internal class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel()
        {
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            PageTitle = "Profile";
            HasBackButton = true;
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
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
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
        #endregion
        #region Functions
        public override Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                UserFullName = $"{GlobalSettings.LoggedUser.FirstName} {GlobalSettings.LoggedUser.LastName}";
                Username = $"{GlobalSettings.LoggedUser.Username}";
                Phone = $"{GlobalSettings.LoggedUser.PhoneNumericCountryCode}{GlobalSettings.LoggedUser.Phone}";
                Email = $"{GlobalSettings.LoggedUser.Email}";
                GlobalSettings.GetCountryList();
                Country = GlobalSettings.CountryList.Where(country => country.CountryCode == GlobalSettings.LoggedUser.Country).FirstOrDefault()?.Name;

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return base.InitializeAsync(navigationData);
        }
        #endregion

    }
}

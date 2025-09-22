using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Controls;
using ConnectCareMobile.Models.Base;
using System;
using System.Collections.Generic;
using ConnectCareMobile.Common.Global;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectCareMobile.Models
{
    public class PersonalInfoModel : BaseModel
    {
        #region Properties
        private List<Selectable> _GenderList = null;
        public List<Selectable> GenderList
        {
            get
            {
                return _GenderList;
            }
            set
            {
                _GenderList = value;
                RaisePropertyChanged();
            }
        }
        private List<Selectable> _CountryList = null;
        public List<Selectable> CountryList
        {
            get
            {
                return _CountryList;
            }
            set
            {
                _CountryList = value;
                RaisePropertyChanged();
            }
        }
        private TextEntryModel _LastName;
        public TextEntryModel LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                RaisePropertyChanged();
            }
        }
        private TextEntryModel _FirstName;
        public TextEntryModel FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public void initializeInputs(Func<Task> CheckCanContinue = null)
        {
            if (GlobalSettings.GenderList == null || GlobalSettings.GenderList?.Count == 0)
            {
                _ = GlobalSettings.GetGenderList();
            }
            if (GlobalSettings.CountryList == null || GlobalSettings.CountryList?.Count == 0)
            {
                _ = GlobalSettings.GetCountryList();
            }
            try
            {
                GenderList = GlobalSettings.GenderList;
                CountryList = GlobalSettings.CountryList?.Select(country => new Selectable()
                {
                    DisplayValue = country.Name,
                    Value = country.CountryCode
                })?.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            FirstName = new TextEntryModel(CheckCanContinue)
            {
                Placeholder = "Enter your first name",
                Title = "First Name*",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
            LastName = new TextEntryModel(CheckCanContinue)
            {
                Placeholder = "Enter your last name",
                Title = "Last Name*",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
        }
        public bool EntriesValid(Selectable Country = null, Selectable Gender = null)
        {
            bool valid = true;
            if (FirstName == null || LastName == null || Country == null || Gender == null)
            {
                valid = false;
            }
            else
            {
                if (string.IsNullOrEmpty(Gender.Value) || string.IsNullOrEmpty(Country.Value) || string.IsNullOrEmpty(LastName.Text) || string.IsNullOrEmpty(FirstName.Text))
                {
                    valid = false;
                }
            }
            return valid;
        }
        #endregion
    }
}

using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Controls;
using ConnectCareMobile.Models.Base;
using System;
using System.Collections.Generic;
using PhoneNumbers;
using System.Net.Mail;
using ConnectCareMobile.Common.Global;
using System.Linq;
using System.Threading.Tasks;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices.Params;
using System.Threading;
using ConnectCareMobile.Services.APIServices;

namespace ConnectCareMobile.Models
{
    public class AccountInfoModel : BaseModel
    {
        private IUserAPIService userAPIService;
        public AccountInfoModel(IUserAPIService userAPIService)
        {
            this.userAPIService = userAPIService;
        }
        #region Properties
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
        private TextEntryModel _Username;
        public TextEntryModel Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                RaisePropertyChanged();
            }
        }
        private TextEntryModel _Password;
        public TextEntryModel Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged();
            }
        }
        private TextEntryModel _ConfirmPassword;
        public TextEntryModel ConfirmPassword
        {
            get
            {
                return _ConfirmPassword;
            }
            set
            {
                _ConfirmPassword = value;
                RaisePropertyChanged();
            }
        }
        private TextEntryModel _Email;
        public TextEntryModel Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                RaisePropertyChanged();
            }
        }
        private TextEntryModel _Phone;
        public TextEntryModel Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public void initializeInputs(Func<Task> CheckCanContinue = null)
        {
            if (GlobalSettings.CountryList == null || GlobalSettings.CountryList?.Count == 0)
            {
                _ = GlobalSettings.GetCountryList();
            }
            try
            {
                CountryList = GlobalSettings.CountryList?.Select(country => new Selectable()
                {
                    DisplayValue = country.CountryCode,
                    DisplayValueLinkName = country.Code,
                    FullDescription = country.Name + $" ({country.Code})",
                    Value = country.CountryCode
                })?.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Username = new TextEntryModel(CheckCanContinue)
            {
                Placeholder = "Enter your username",
                IsPassword = false,
                Title = "Username*",
                ErrorText = "",
                IsError = false,
                Text = ""
            };
            Password = new TextEntryModel(CheckCanContinue)
            {
                Title = "Password*",
                Placeholder = "Enter your password",
                IsPassword = true,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
            ConfirmPassword = new TextEntryModel(CheckCanContinue)
            {
                Placeholder = "Enter your password again",
                Title = "Confirm password*",
                IsPassword = true,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
            Email = new TextEntryModel(CheckCanContinue)
            {
                Placeholder = "Enter your email address",
                IsPassword = false,
                Title = "Email",
                ErrorText = "",
                IsError = false,
                Text = ""
            };
            Phone = new TextEntryModel(CheckCanContinue)
            {
                Placeholder = "Enter your phone",
                IsPassword = false,
                Title = "Phone Number*",
                ErrorText = "",
                IsError = false,
                Text = ""
            };
        }
        public bool EntriesValid(Selectable Country = null)
        {
            bool valid = true;
            if (Username == null || Phone == null || Country == null || Password == null || ConfirmPassword == null)
            {
                valid = false;
            }
            else
            {
                if (string.IsNullOrEmpty(ConfirmPassword.Text) || string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(Phone.Text) || string.IsNullOrEmpty(Username.Text))
                {
                    valid = false;
                }
            }
            return valid;
        }
        public bool ValidateData(Selectable Country = null)
        {
            bool UsernameValid = true;
            bool PhoneValid = true;
            bool PasswordValid = true;
            bool ConfirmPasswordValid = true;
            bool EmailValid = true;
            bool valid = true;
            if (!string.IsNullOrEmpty(Email.Text))
            {
                try
                {
                    new MailAddress(Email.Text);
                    Email.IsError = false;
                }
                catch (Exception ex)
                {
                    EmailValid = false;
                    Email.IsError = true;
                    Email.ErrorText = "Invalid email";
                }
            }
            if (!Password.Text.Equals(ConfirmPassword.Text))
            {
                ConfirmPasswordValid = false;
                ConfirmPassword.IsError = true;
                ConfirmPassword.ErrorText = "Passwords doesnt match";
            }
            else
            {
                ConfirmPassword.IsError = false;
            }
            try
            {
                PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
                PhoneNumber phoneNumber = phoneNumberUtil.Parse(Phone.Text, Country?.DisplayValue);
                PhoneValid = phoneNumberUtil.IsValidNumber(phoneNumber);

                if (!PhoneValid)
                {
                    Phone.ErrorText = "Invalid phone number";
                    Phone.IsError = true;
                }
                else
                {
                    Phone.IsError = !PhoneValid;
                }
            }
            catch (Exception ex)
            {
                PhoneValid = false;
                Phone.ErrorText = "Invalid phone number";
                Phone.IsError = true;
            }
            valid = UsernameValid && PhoneValid && PasswordValid && ConfirmPasswordValid && EmailValid;
            return valid;
        }
        public async Task<Response<CreateAccountResponse>> CreateUser(CreateUserParams userParams, CancellationToken cancellationToken)
        {
            var response = await userAPIService.CreateAccount(userParams, cancellationToken);
            return response;
        }
        public async Task<Response<CreateAccountResponse>> CreateAutisticUser(CreateUserParams userParams, CancellationToken cancellationToken)
        {
            return await userAPIService.CreateAutisticAccount(userParams, cancellationToken);
        }
        #endregion
    }
}

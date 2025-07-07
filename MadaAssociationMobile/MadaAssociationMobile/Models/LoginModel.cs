using MadaAssociationMobile.Controls.Entries;
using MadaAssociationMobile.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models.Base;
using MadaAssociationMobile.Services.APIServices.Params;
using MadaAssociationMobile.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Models
{
    public class LoginModel : BaseModel
    {
        IUserAPIService userAPIService;

        public LoginModel(IUserAPIService userAPIService)
        {
            this.userAPIService = userAPIService;
        }

        #region Properties
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
        #endregion
        #region Functions
        public void initializeInputs(Func<Task> CheckCanSignIn = null)
        {
            Username = new TextEntryModel(CheckCanSignIn)
            {
                Placeholder = "Enter your username",
                Title = "Username",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
            Password = new TextEntryModel(CheckCanSignIn)
            {
                Placeholder = "Enter password",
                Title = "Password",
                IsPassword = true,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
        }
        public bool ValidateEntries()
        {
            bool valid = true;
            if (Username == null || Password == null)
            {
                valid = false;
            }
            else
            {
                if (string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text))
                {
                    valid = false;
                }
            }
            return valid;
        }
        public async Task<Response<LoginResponse>> Login(LoginAPIParams userParams, CancellationToken cancellationToken)
        {
            var response = await userAPIService.Login(userParams, cancellationToken);
            return response;
        }

        #endregion
    }
}

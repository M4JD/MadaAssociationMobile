using MadaAssociationMobile.Controls.Entries;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models.Base;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Params;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Models
{
    public class VerifyPhoneNumberModel : BaseModel
    {

        IUserAPIService userAPIService = null;
        public VerifyPhoneNumberModel(IUserAPIService userAPIService)
        {
            this.userAPIService = userAPIService;
        }
        #region Properties
        private TextEntryModel _OTP;
        public TextEntryModel OTP
        {
            get
            {
                return _OTP;
            }
            set
            {
                _OTP = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public void initializeInputs(Func<Task> CheckButtonState = null)
        {
            OTP = new TextEntryModel(CheckButtonState)
            {
                Placeholder = "Enter code",
                Title = "Please enter the sms code you received",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
        }
        public bool EntriesValid()
        {
            bool valid = true;
            if (OTP == null || string.IsNullOrEmpty(OTP.Text))
            {
                valid = false;
            }
            return valid;
        }
        public async Task<Response<VerifyPhoneNumberResponse>> VerifyPhoneNumberAsync(VerifyPhoneNumberParams Params, CancellationToken cancellationToken)
        {
            return await userAPIService.VerifyPhoneNumber(Params, cancellationToken);
        }
        #endregion
    }
}

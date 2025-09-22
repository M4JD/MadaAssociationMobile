using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models.Base;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.Models
{
    public class CareTakerInfoModel : BaseModel
    {

        IUserAPIService userAPIService = null;
        public CareTakerInfoModel(IUserAPIService userAPIService)
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
        #endregion
        #region Functions
        public void initializeInputs(Func<Task> CheckButtonState = null)
        {
            Username = new TextEntryModel(CheckButtonState)
            {
                Placeholder = "Enter caretaker username",
                Title = "Please enter your caretaker’s username",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
        }
        public bool EntriesValid()
        {
            bool valid = true;
            if (Username == null || string.IsNullOrEmpty(Username.Text))
            {
                valid = false;
            }
            return valid;
        }
        public async Task<Response<CreateAccountResponse>> UpdateAutisticAccountCareTaker(UpdateAutisticAccountCareTakerParams userParams, CancellationToken cancellationToken)
        {
            return await userAPIService.UpdateAutisticAccountCareTaker(userParams, cancellationToken);
        }
        #endregion
    }
}

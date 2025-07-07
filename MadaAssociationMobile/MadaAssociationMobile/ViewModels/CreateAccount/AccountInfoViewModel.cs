using MadaAssociationMobile.Controls;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Params;
using MadaAssociationMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MadaAssociationMobile.ViewModels
{
    public class AccountInfoViewModel : BaseViewModel
    {
        INavigationService navigationService = null;
        public AccountInfoViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = ViewModelLocator.Resolve<AccountInfoModel>();
        }
        private CancellationToken cancellationToken = new CancellationToken();
        private CreateUserParams UserData = null;
        #region Properties
        private AccountInfoModel _Model = null;
        public AccountInfoModel Model
        {
            get
            {
                return _Model;
            }
            set
            {
                _Model = value;
                RaisePropertyChanged();
            }
        }
        private Selectable _Country = new Selectable();
        public Selectable Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
                NextCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
        private string _MaxSteps = "Step 2 of 3: ";
        public string MaxSteps
        {
            get { return _MaxSteps; }
            set
            {
                _MaxSteps = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public override async Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                if (!navigationData.ContainsKey("UserData") || navigationData["UserData"] == null)
                {
                    Debug.WriteLine("Invalid params");
                    await navigationService.NavigateBack();
                    return await Task.FromResult(false);
                }
                else
                {
                    Model.initializeInputs(CheckCanContinue);
                    UserData = (CreateUserParams)navigationData["UserData"];
                    if (UserData.IsAutistic)
                    {
                        MaxSteps = "Step 2 of 4: ";
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return await Task.FromResult(true);
        }
        private Task CheckCanContinue()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NextCommand?.RaiseCanExecuteChanged();
            });
            return Task.CompletedTask;
        }
        #endregion
        #region Commands
        private CommandAsync _NextCommand = null;
        public CommandAsync NextCommand
        {
            get
            {
                return _NextCommand ??
                 (_NextCommand = new CommandAsync(execute: async () =>
                    {
                        if (Model.ValidateData(Country))
                        {
                            try
                            {
                                IsLoading = true;
                                UserData.Username = Model.Username.Text;
                                UserData.Password = Model.Password.Text;
                                UserData.Phone = Model.Phone.Text;
                                UserData.PhoneCountryCode = Country.Value;
                                UserData.PhoneNumericCountryCode = Country.DisplayValueLinkName;
                                UserData.Email = Model.Email?.Text;

                                Dictionary<string, object> data = new Dictionary<string, object>();
                                data.Add("UserData", UserData);
                                Response<CreateAccountResponse> response = new Response<CreateAccountResponse>();
                                if (UserData.IsAutistic)
                                {
                                    response = await Model.CreateAutisticUser(UserData, cancellationToken);
                                }
                                else
                                {
                                    response = await Model.CreateUser(UserData, cancellationToken);
                                }
                                var result = response.HandleCreateAccountAPICall();
                                if (result.Status)
                                {
                                    await navigationService.NavigateTo<VerifyPhoneNumberViewModel>(data, false);
                                }
                                else
                                {
                                    Debug.Write(result.Message);
                                }
                            }
                            catch (Exception ex)
                            {
                                HandleException(ex);
                            }
                            finally { IsLoading = false; }
                        }
                    },
                    canExecute: () =>
                    {
                        return Model.EntriesValid(Country);
                    }));

            }
        }
        private CommandAsync _GoBackCommand = null;
        public CommandAsync GoBackCommand
        {
            get
            {
                return _GoBackCommand ??
                 (_GoBackCommand = new CommandAsync(execute: async () =>
                    {
                        await navigationService.NavigateTo<LoginViewModel>(null, true);
                    }));

            }
        }
        #endregion

    }
}

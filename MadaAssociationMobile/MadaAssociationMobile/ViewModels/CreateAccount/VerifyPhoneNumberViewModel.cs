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
    public class VerifyPhoneNumberViewModel : BaseViewModel
    {
        INavigationService navigationService = null;
        public VerifyPhoneNumberViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = ViewModelLocator.Resolve<VerifyPhoneNumberModel>();
        }
        private CreateUserParams UserData = null;
        private CancellationToken cancellationToken = new CancellationToken();
        #region Properties
        private string _MaxSteps = "Step 3 of 3: ";
        public string MaxSteps
        {
            get { return _MaxSteps; }
            set
            {
                _MaxSteps = value;
                RaisePropertyChanged();
            }
        }
        private VerifyPhoneNumberModel _Model;
        public VerifyPhoneNumberModel Model
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
                        MaxSteps = "Step 3 of 4: ";
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
                VerifyOtpCommand?.RaiseCanExecuteChanged();
            });
            return Task.CompletedTask;
        }
        #endregion
        #region Commands
        private CommandAsync _VerifyOtpCommand = null;
        public CommandAsync VerifyOtpCommand
        {
            get
            {
                return _VerifyOtpCommand ??
                (_VerifyOtpCommand = new CommandAsync(execute: async () =>
                    {
                        try
                        {
                            IsLoading = true;
                            LoadingText = "Loading...";
                            var result = await Model.VerifyPhoneNumberAsync(new VerifyPhoneNumberParams()
                            {
                                Username = UserData.Username,
                                OTP = Model.OTP.Text
                            }, cancellationToken);
                            var response = result.HandleVerifyPhoneNumberAPICall();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("UserData", UserData);
                            if (response.Status)
                            {
                                if (UserData.IsAutistic)
                                {
                                    await navigationService.NavigateTo<CareTakerInfoViewModel>(data, false);
                                }
                                else
                                    await navigationService.NavigateToRoot();
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                        finally
                        {
                            IsLoading = false;
                        }
                    },
                    canExecute: () =>
                    {
                        return Model.EntriesValid();
                    }));
            }
        }
        #endregion
    }
}

using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    internal class CareTakerInfoViewModel : BaseViewModel
    {
        INavigationService navigationService = null;
        public CareTakerInfoViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = ViewModelLocator.Resolve<CareTakerInfoModel>();
        }
        private CreateUserParams UserData = null;
        private CancellationToken cancellationToken = new CancellationToken();
        #region Properties
        private CareTakerInfoModel _Model;
        public CareTakerInfoModel Model
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
                SignUpCommand?.RaiseCanExecuteChanged();
            });
            return Task.CompletedTask;
        }
        #endregion
        #region Commands
        private CommandAsync _SignUpCommand = null;
        public CommandAsync SignUpCommand
        {
            get
            {
                return _SignUpCommand ??
                    (_SignUpCommand = new CommandAsync(execute: async () =>
                    {
                        try
                        {
                            IsLoading = true;
                            UserData.CareTakerUsername = Model.Username.Text;
                            var result = await Model.UpdateAutisticAccountCareTaker(new UpdateAutisticAccountCareTakerParams()
                            {
                                CareTakerUsername = UserData.CareTakerUsername,
                                Username = UserData.Username
                            }, cancellationToken);
                            var reponse = result.HandleCreateAccountAPICall();
                            if (reponse.Status)
                            {
                                await navigationService.NavigateToRoot();
                            }
                            else
                            {
                                Debug.Write(reponse.Message);
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConnectCareMobile.APIServices;
using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.NavigationServices;
using ConnectCareMobile.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        INavigationService navigationService = null;
        CancellationToken cancellationToken = new CancellationToken();
        public LoginViewModel(
            INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = ViewModelLocator.Resolve<LoginModel>();
        }
        #region Properties
        private string _ErrorMessage = null;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                RaisePropertyChanged();
            }
        }
        private LoginModel _Model = null;
        public LoginModel Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public override Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            Model.initializeInputs(CheckCanSignIn);
            return Task.FromResult(true);
        }

        private Task CheckCanSignIn()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                LoginCommand?.RaiseCanExecuteChanged();
            });
            return Task.CompletedTask;
        }
        #endregion
        #region Commands
        private CommandAsync _LoginCommand;
        public CommandAsync LoginCommand
        {
            get
            {

                return _LoginCommand ??
                    (_LoginCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            IsLoading = true;
                            LoadingText = "Loading...";
                            var response = await Model.Login(new Services.APIServices.Params.LoginAPIParams()
                            {
                                Username = Model.Username.Text.Trim(),
                                Password = Model.Password.Text.Trim()
                            }, cancellationToken);

                            var results = response.HandleLoginAPICall();
                            if (results.Status)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var status = await CheckMicrophonePermission();
                                    if (status)
                                    {
                                    }
                                    else
                                    {
                                        status = await AskForMicrophonePermission();
                                        if (status)
                                        {
                                        }
                                    }
                                    var sinchService = DependencyService.Get<ISinchService>();
                                    sinchService.InitializeChatSDK(results.Response.UserId.ToString());
                                });

                                AppSharedPrefences.Instance.IsAutistic = results.Response.IsAutistic;
                                AppSharedPrefences.Instance.IsLoggedIn = true;
                                AppSharedPrefences.Instance.UserId = results.Response.UserId.ToString();
                                
                                AppSharedPrefences.Instance.LoggedInUser = JsonConvert.SerializeObject(results.Response);
                                GlobalSettings.LoggedUser = results.Response;
                                Model.Username.IsError = false;
                                Model.Password.IsError = false;
                                if (!AppSharedPrefences.Instance.IsAutistic)
                                {
                                    await navigationService.NavigateTo<MainViewModel>(null, true);
                                }
                                else
                                    await navigationService.NavigateTo<MessagesListViewModel>(null, true);
                            }
                            else
                            {
                                ErrorMessage = results.Message;
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                        finally
                        {
                            IsLoading = false;
                            LoadingText = "";
                        }
                        //var firebaseMessagingHelper = DependencyService.Get<IFirebaseMessagingHelper>();
                        //var FCM = firebaseMessagingHelper.GetFCMToken();
                        //MirrorflyService.RegisterUser(Guid.NewGuid().ToString(), FCM);
                        //await navigationService.NavigateTo<RoomViewModel>(null, false);
                        //var status = await CheckMicrophonePermission();
                        //var AudioRecorder = DependencyService.Get<IAudioRecorder>();
                        //if (status)
                        //{
                        //    AudioRecorder.StartRecording();
                        //}
                        //else
                        //{
                        //    status = await AskForMicrophonePermission();
                        //}
                        //if (status)
                        //{
                        //    AudioRecorder.StartRecording();
                        //}
                        //await Task.Delay(10000);
                        //AudioRecorder.StopRecording();
                        //AudioRecorder.StartPlaying();

                        //await navigationService.NavigateTo<ContactsViewModel>(null, false);
                    }, () =>
                        {
                            return Model.ValidateEntries(); ;
                        }));
            }
        }
        private CommandAsync _CreateAccountCommand = null;
        public CommandAsync CreateAccountCommand
        {
            get
            {
                return _CreateAccountCommand ?? (_CreateAccountCommand = new CommandAsync(execute: async () =>
                {
                    try
                    {
                        await navigationService.NavigateTo<AccountTypeViewModel>(null);
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }));
            }
        }
        private CommandAsync _ForgetPasswordCommand = null;
        public CommandAsync ForgetPasswordCommand
        {
            get
            {
                return _ForgetPasswordCommand ?? (_ForgetPasswordCommand = new CommandAsync(execute: async () =>
                 {
                     try
                     {
                         await navigationService.NavigateTo<ForgetPasswordViewModel>(null);
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

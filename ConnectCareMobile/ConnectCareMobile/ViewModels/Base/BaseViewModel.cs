using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models;
using ConnectCareMobile.Services.NavigationServices;
using ConnectCareMobile.ViewModels.Base;
using ConnectCareMobile.Views;
using ConnectCareMobile.Views.Base;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ConnectCareMobile.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private INavigationService navigationService;
        public BaseViewModel()
        {
            try
            {
                this.navigationService = ViewModelLocator.Resolve<INavigationService>();
            }
            catch (Exception ex) { }
        }
        #region Properties
        private CancellationToken cancellationToken = new CancellationToken();
        private Color _NavBarBackGroundColor = Color.White;
        public Color NavBarBackGroundColor
        {
            get
            {
                return _NavBarBackGroundColor;
            }
            set
            {
                _NavBarBackGroundColor = value;
                RaisePropertyChanged();
            }
        }
        private bool _HasNavBarAddFriendButton = false;
        public bool HasNavBarAddFriendButton
        {
            get
            {
                return _HasNavBarAddFriendButton;
            }
            set
            {
                _HasNavBarAddFriendButton = value;
                RaisePropertyChanged();
            }
        }
        private bool _RequestSent = false;
        public bool RequestSent
        {
            get
            {
                return _RequestSent;
            }
            set
            {
                _RequestSent = value;
                RaisePropertyChanged();
            }
        }
        private bool _HasBackButton = false;
        public bool HasBackButton
        {
            get
            {
                return _HasBackButton;
            }
            set
            {
                _HasBackButton = value;
                RaisePropertyChanged();
            }
        }
        private bool _HasTabBar = false;
        public bool HasTabBar
        {
            get
            {
                return _HasTabBar;
            }
            set
            {
                _HasTabBar = value;
                RaisePropertyChanged();
            }
        }
        private bool _HasNavBar = false;
        public bool HasNavBar
        {
            get
            {
                return _HasNavBar;
            }
            set
            {
                _HasNavBar = value;
                RaisePropertyChanged();
            }
        }
        private bool _HasNavBarOptionButton = false;
        public bool HasNavBarOptionButton
        {
            get
            {
                return _HasNavBarOptionButton;
            }
            set
            {
                _HasNavBarOptionButton = value;
                RaisePropertyChanged();
            }
        }
        private bool _HasNavBarVideoCallButton = false;
        public bool HasNavBarVideoCallButton
        {
            get
            {
                return _HasNavBarVideoCallButton;
            }
            set
            {
                _HasNavBarVideoCallButton = value;
                RaisePropertyChanged();
            }
        }
        private bool _HasNavBarVoiceCallButton = false;
        public bool HasNavBarVoiceCallButton
        {
            get
            {
                return _HasNavBarVoiceCallButton;
            }
            set
            {
                _HasNavBarVoiceCallButton = value;
                RaisePropertyChanged();
            }
        }
        private StatusBar _StatusBar = new StatusBar();
        public StatusBar StatusBar
        {
            get
            {
                return _StatusBar;
            }
            set
            {
                _StatusBar = value;
                RaisePropertyChanged();
            }
        }
        private string _PageTitle;
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                _PageTitle = value;
                RaisePropertyChanged();
            }
        }
        private string _LoadingText;
        public string LoadingText
        {
            get
            {
                return _LoadingText;
            }
            set
            {
                _LoadingText = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsProfileView = false;
        public bool IsProfileView
        {
            get
            {
                return _IsProfileView;
            }
            set
            {
                _IsProfileView = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsTitleView = false;
        public bool IsTitleView
        {
            get
            {
                return _IsTitleView;
            }
            set
            {
                _IsTitleView = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsPopUpActive = false;
        public bool IsPopUpActive
        {
            get
            {
                return _IsPopUpActive;
            }
            set
            {
                _IsPopUpActive = value;
                RaisePropertyChanged();
            }
        }
        public bool PageEnabled
        {
            get
            {
                return IsLoading || IsActionSheetActive || IsPopUpActive;
            }
        }
        private bool _IsInflatorMenuActive = false;
        public bool IsInflatorMenuActive
        {
            get
            {
                return _IsInflatorMenuActive;
            }
            set
            {
                _IsInflatorMenuActive = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsActionSheetActive = false;
        public bool IsActionSheetActive
        {
            get
            {
                return _IsActionSheetActive;
            }
            set
            {
                _IsActionSheetActive = value;
                RaisePropertyChanged();
            }
        }

        public string ChatReceiverName
        {
            get
            {
                return _ChatReceiverName;
            }
            set
            {
                _ChatReceiverName = value;
                RaisePropertyChanged();
            }
        }
        private string _ChatReceiverName = string.Empty;
        public string ChatReceiverStatus
        {
            get
            {
                return _ChatReceiverStatus;
            }
            set
            {
                _ChatReceiverStatus = value;
                RaisePropertyChanged();
            }
        }
        private string _ChatReceiverStatus = string.Empty;
        private TextEntryModel _ContactUsername = new TextEntryModel()
        {
            Placeholder = "Enter your username",
            Title = "Username",
            IsPassword = false,
            ErrorText = "",
            IsError = false,
            Text = ""
        };
        public TextEntryModel ContactUsername
        {
            get
            {
                return _ContactUsername;
            }
            set
            {
                _ContactUsername = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<Base.MenuItem> _MenuItems = new ObservableCollection<Base.MenuItem>();
        public ObservableCollection<Base.MenuItem> MenuItems
        {
            get
            {
                return _MenuItems;
            }
            set
            {
                _MenuItems = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Commands
        private CommandAsync _ClosePopUpCommand;
        public CommandAsync ClosePopUpCommand
        {
            get
            {

                return _ClosePopUpCommand ??
                    (_ClosePopUpCommand = new CommandAsync(async () =>
                    {
                        IsPopUpActive = false;
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _OptionsButtonCommand;
        public CommandAsync OptionsButtonCommand
        {
            get
            {

                return _OptionsButtonCommand ??
                    (_OptionsButtonCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            IsInflatorMenuActive = true;
                        }
                        catch (Exception ex) { }
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _VoiceCallButtonCommand;
        public virtual CommandAsync VoiceCallButtonCommand
        {
            get
            {

                return _VoiceCallButtonCommand ??
                    (_VoiceCallButtonCommand = new CommandAsync(async () =>
                    {
                        try
                        {

                        }
                        catch (Exception ex) { }
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _VideoCallButtonCommand;
        public virtual CommandAsync VideoCallButtonCommand
        {
            get
            {

                return _VideoCallButtonCommand ??
                    (_VoiceCallButtonCommand = new CommandAsync(async () =>
                    {
                        try
                        {

                        }
                        catch (Exception ex) { }
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _SubmitPopupCommand;
        public virtual CommandAsync SubmitPopupCommand
        {
            get
            {

                return _SubmitPopupCommand ??
                    (_SubmitPopupCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            IsPopUpActive = false;
                        }
                        catch (Exception ex) { }
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _AddFriendButtonCommand;
        public CommandAsync AddFriendButtonCommand
        {
            get
            {

                return _AddFriendButtonCommand ??
                    (_AddFriendButtonCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            IsPopUpActive = true;
                        }
                        catch (Exception ex) { }
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _BackButtonCommand;
        public virtual CommandAsync BackButtonCommand
        {
            get
            {

                return _BackButtonCommand ??
                    (_BackButtonCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            if (IsPopUpActive)
                            {
                                IsPopUpActive = false;
                                return;
                            }
                            await navigationService.NavigateBack();
                        }
                        catch (Exception ex) { }
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _HideActionSheetCommand;
        public CommandAsync HideActionSheetCommand
        {
            get
            {

                return _HideActionSheetCommand ??
                    (_HideActionSheetCommand = new CommandAsync(async () =>
                    {
                        IsActionSheetActive = false;
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        private CommandAsync _HideInflatorMenuCommand;
        public CommandAsync HideInflatorMenuCommand
        {
            get
            {

                return _HideInflatorMenuCommand ??
                    (_HideInflatorMenuCommand = new CommandAsync(async () =>
                    {
                        IsInflatorMenuActive = false;
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        #endregion

        #region Functions
        public virtual Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // update fcm for user device
                    if (AppSharedPrefences.Instance.FCMTokenChanged)
                    {
                        var result = await ViewModelLocator.Resolve<IUserAPIService>().UpdateDeviceFCM(new Services.APIServices.Params.UpdateDeviceFCMParams()
                        {
                            FCM = GlobalSettings.FCM,
                            UserId = GlobalSettings.LoggedUser.UserId
                        }, cancellationToken);
                        if (result.Status == HttpStatusCode.OK)
                        {
                            AppSharedPrefences.Instance.FCMTokenChanged = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            });
            return Task.FromResult(true);
        }
        public virtual Task<bool> OnAppearing(BaseView baseView)
        {
            return Task.FromResult(true);
        }
        public virtual void OnDisappearing()
        {
        }
        public async Task<bool> CheckContactsPermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.ContactsRead>();
                return await Task.FromResult(status == PermissionStatus.Granted);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> AskForContactsPermission()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.ContactsRead>();
                return await Task.FromResult(status == PermissionStatus.Granted);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> CheckMicrophonePermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
                return await Task.FromResult(status == PermissionStatus.Granted);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> AskForMicrophonePermission()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Microphone>();
                return await Task.FromResult(status == PermissionStatus.Granted);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> CheckCameraPermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                return await Task.FromResult(status == PermissionStatus.Granted);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> AskForCameraPermission()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                return await Task.FromResult(status == PermissionStatus.Granted);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return await Task.FromResult(false);
            }
        }
        public void HandleException(Exception ex)
        {
            Crashes.TrackError(ex);
        }
        #endregion
    }

}

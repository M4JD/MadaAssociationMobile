using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.NavigationServices;
using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        private CancellationToken cancellationToken = new CancellationToken();
        public ContactsViewModel(
            INavigationService navigationService,
            ContactsModel contactsModel)
        {
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBarAddFriendButton = true;
            HasNavBar = true;
            PageTitle = "Contacts";
            HasBackButton = true;
            var color = ColorConverters.FromHex("#2965FF");
            StatusBar = new StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            this.navigationService = navigationService;
            Model = contactsModel;
        }
        #region Properties
        private ContactsModel _Model;
        public ContactsModel Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                RaisePropertyChanged();
            }
        }
        string _AcceptButtonText = string.Empty;
        public string AcceptButtonText
        {
            get { return _AcceptButtonText; }
            set
            {
                _AcceptButtonText = value;
                RaisePropertyChanged();
            }
        }
        string _RejectButtonText = string.Empty;
        public string RejectButtonText
        {
            get { return _RejectButtonText; }
            set
            {
                _RejectButtonText = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<MemberResponse> _ContactsList = new ObservableCollection<MemberResponse>();
        public ObservableCollection<MemberResponse> ContactsList
        {
            get
            {
                return _ContactsList;
            }
            set
            {
                _ContactsList = value;
                RaisePropertyChanged();
            }
        }
        bool _IsRefreshing = false;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set
            {
                _IsRefreshing = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        private bool ValidateEntries()
        {
            bool valid = true;
            if (ContactUsername == null)
            {
                valid = false;
            }
            else
            {
                if (string.IsNullOrEmpty(ContactUsername.Text))
                {
                    valid = false;
                }
            }
            return valid;
        }
        private Task CheckCanContinue()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SubmitPopupCommand?.RaiseCanExecuteChanged();
            });
            return Task.CompletedTask;
        }
        public override async Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                AcceptButtonText = "Accept";
                RejectButtonText = "Reject";
                ContactUsername = new TextEntryModel(CheckCanContinue)
                {
                    Title = "Username",
                    Text = "",
                    Placeholder = "Enter username"
                };
                RefreshDataCommand?.ExecuteAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> GetContacts()
        {
            try
            {
                var contacts = await Contacts.GetAllAsync(cancellationToken);
                if (contacts == null)
                    return false;

                //foreach (var contact in contacts)
                //ContactsList.Add(contact);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
            return true;
        }
        #endregion
        #region Commands
        private CommandAsync _RefreshDataCommand;
        public CommandAsync RefreshDataCommand
        {
            get
            {
                return _RefreshDataCommand ?? (_RefreshDataCommand = new CommandAsync(async () =>
                {
                    try
                    {
                        IsRefreshing = true;
                        var contacts = await Model.GetConnections(GlobalSettings.LoggedUser.UserId);
                        if (contacts != null)
                        {
                            ContactsList = new ObservableCollection<MemberResponse>(contacts);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        HandleException(ex);
                    }
                    finally
                    {
                        IsRefreshing = false;
                    }
                }));
            }
        }
        private CommandAsync _SubmitPopupCommand;
        public override CommandAsync SubmitPopupCommand
        {
            get
            {
                return _SubmitPopupCommand ??
                    (_SubmitPopupCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            IsLoading = true;
                            LoadingText = "Loading...";
                            var res = await Model.SendConnectionRequest(ContactUsername.Text);
                            if (res)
                            {
                                ContactUsername.Text = "";
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    RequestSent = true;
                                    await Task.Delay(1000);
                                    RequestSent = false;
                                    IsPopUpActive = false;
                                });
                            }
                            else
                            {
                                ContactUsername.IsError = true;
                                ContactUsername.ErrorText = "Invalid username.";
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                            HandleException(ex);
                        }
                        finally
                        {
                            IsLoading = false;
                            LoadingText = "";
                        }
                    }, () =>
                    {
                        return ValidateEntries();
                    }));
            }
        }
        private CommandAsync<object> _ContactClickedCommand;
        public CommandAsync<object> ContactClickedCommand
        {
            get
            {

                return _ContactClickedCommand ??
                    (_ContactClickedCommand = new CommandAsync<object>(async (member) =>
                    {
                        try
                        {
                            IsLoading = true;
                            LoadingText = "Loading...";
                            var user = (member as MemberResponse);
                            var ChatId = await Model.AddChat(user.UserId);
                            var data = new Dictionary<string, object>();
                            var Response = new ChatResponse()
                            {
                                ChatId = ChatId,
                                Receiver = user,
                                SenderId = GlobalSettings.LoggedUser.UserId
                            };
                            data.Add("MemberInfo", Response);

                            await navigationService.NavigateTo<ChatDetailsViewModel>(data, true);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                            Debug.WriteLine(ex);
                        }
                        finally
                        {
                            IsLoading = false;
                            LoadingText = "";
                        }
                    }));
            }
        }
        private CommandAsync<MemberResponse> _ApproveCommand;
        public CommandAsync<MemberResponse> ApproveCommand
        {
            get
            {
                return _ApproveCommand ?? (_ApproveCommand = new CommandAsync<MemberResponse>(async (data) =>
                {
                    try
                    {
                        IsLoading = true;
                        LoadingText = "Loading...";
                        var membersList = await Model.AcceptConnection(data.ConnectionId);
                        if (membersList)
                        {
                            _ContactsList.Remove(data);
                            data.IsAccepted = true;
                            _ContactsList.Add(data);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        HandleException(ex);
                    }
                    finally
                    {
                        IsLoading = false;
                        LoadingText = "";
                    }
                }));
            }
        }
        private CommandAsync<MemberResponse> _RejectCommand;
        public CommandAsync<MemberResponse> RejectCommand
        {
            get
            {
                return _RejectCommand ?? (_RejectCommand = new CommandAsync<MemberResponse>(async (data) =>
                {
                    try
                    {
                        IsLoading = true;
                        LoadingText = "Loading...";
                        var membersList = await Model.RejectConnection(data.ConnectionId);
                        if (membersList)
                        {
                            _ContactsList.Remove(data);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        HandleException(ex);
                    }
                    finally
                    {
                        IsLoading = false;
                        LoadingText = "";
                    }
                }));
            }
        }
        #endregion
    }
}

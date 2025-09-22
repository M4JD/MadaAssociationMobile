using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    public class UserManagementViewModel : BaseViewModel
    {
        public UserManagementViewModel(INavigationService navigationService)
        {
            Model = ViewModelLocator.Resolve<UserManagementModel>();
            var color = ColorConverters.FromHex("#2965FF");
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            HasBackButton = false;
            StatusBar = new StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            HasTabBar = !AppSharedPrefences.Instance.IsAutistic;
            PageTitle = "Autistic User Management";
            _navigationService = navigationService;
        }
        #region Properties
        INavigationService _navigationService = null;
        CancellationToken cancellationToken = new CancellationToken();
        UserManagementModel _Model = null;
        public UserManagementModel Model
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

        ObservableCollection<MemberResponse> _Members = null;
        public ObservableCollection<MemberResponse> Members
        {
            get { return _Members; }
            set
            {
                _Members = value;
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
        public override async Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                AcceptButtonText = "Accept";
                RejectButtonText = "Reject";
                IsLoading = true;
                LoadingText = "Loading...";
                await RefreshDataCommand?.ExecuteAsync();
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
            return await base.InitializeAsync(navigationData);

        }
        #endregion
        #region Commands
        private CommandAsync<MemberResponse> _ViewUserCommand;
        public CommandAsync<MemberResponse> ViewUserCommand
        {
            get
            {
                return _ViewUserCommand ?? (_ViewUserCommand = new CommandAsync<MemberResponse>(async (data) =>
                {
                    Dictionary<string, object> pageparams = new Dictionary<string, object>();
                    pageparams.Add("MemberInfo", data);
                    await _navigationService.NavigateTo<UserProfileManagementViewModel>(pageparams);
                }));
            }
        }
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
                        var membersList = await Model.GetAutisticUsers(cancellationToken);
                        if (membersList != null)
                        {
                            Members = new ObservableCollection<MemberResponse>(membersList);
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
                        var membersList = await Model.ApproveRequest(data.MemberId, cancellationToken);
                        if (membersList)
                        {
                            Members.Remove(data);
                            data.IsApproved = true;
                            Members.Add(data);
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
                        var membersList = await Model.RejectRequest(data.MemberId, cancellationToken);
                        if (membersList)
                        {
                            Members.Remove(data);
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

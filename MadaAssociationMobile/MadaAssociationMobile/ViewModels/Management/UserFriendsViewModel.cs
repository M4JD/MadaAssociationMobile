using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace MadaAssociationMobile.ViewModels
{
    public class UserFriendsViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        private CancellationToken cancellationToken = new CancellationToken();
        public UserFriendsViewModel(INavigationService navigationService)
        {
            var color = ColorConverters.FromHex("#2965FF");
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            PageTitle = "Autistic User Management (Friends)";
            HasBackButton = true;
            HasNavBarOptionButton = false;
            StatusBar = new StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            HasTabBar = false;
            Model = ViewModelLocator.Resolve<UserFriendsModel>();
            this.navigationService = navigationService;
        }
        #region Properties
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
        private UserFriendsModel _Model = null;
        public UserFriendsModel Model
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
        private MemberResponse _User = null;
        public MemberResponse User
        {
            get { return _User; }
            set
            {
                _User = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public override async Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                AcceptButtonText = "Approve";
                RejectButtonText = "Disapprove";
                if (navigationData != null && navigationData.ContainsKey("MemberInfo"))
                {
                    User = navigationData["MemberInfo"] as MemberResponse;
                    IsLoading = true;
                    LoadingText = "Loading...";
                    Members = new ObservableCollection<MemberResponse>(await Model.GetAutisticUserConnections(User.UserId, cancellationToken));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                LoadingText = "";
                IsLoading = false;
            }
            return await base.InitializeAsync(navigationData);
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
                        if (User != null && User.UserId != Guid.Empty)
                            Members = new ObservableCollection<MemberResponse>(await Model.GetAutisticUserConnections(User.UserId, cancellationToken));
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
                        var membersList = await Model.ApproveConnection(data.ConnectionId, cancellationToken);
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
                        var membersList = await Model.DisapproveConnection(data.ConnectionId, cancellationToken);
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

using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Response;
using MadaAssociationMobile.ViewModels.Base;
using MadaAssociationMobile.Views.Base;
using Org.BouncyCastle.Asn1.Cmp;
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

namespace MadaAssociationMobile.ViewModels
{
    public class MessagesListViewModel : BaseViewModel
    {
        public MessagesListViewModel(INavigationService navigationService)
        {
            HasBackButton = false;
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            HasNavBarOptionButton = true;
            PageTitle = "Messages";
            Model = ViewModelLocator.Resolve<MessagesListModel>();
            var color = ColorConverters.FromHex("#2965FF");
            StatusBar = new Base.StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            this.navigationService = navigationService;
            MenuItems = new ObservableCollection<Base.MenuItem>() { new Base.MenuItem() {
                Text = "Contacts",
                Command = ContactsCommand
            }};
            if (AppSharedPrefences.Instance.IsAutistic)
                MenuItems.Add(
                    new Base.MenuItem()
                    {
                        Text = "Settings",
                        Command = SettingsCommand
                    });
        }
        #region Properties
        private bool _Active = false;
        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsRefreshing = false;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set
            {
                _IsRefreshing = value;

                RaisePropertyChanged();
            }
        }
        private MessagesListModel _Model;
        public MessagesListModel Model
        {
            get => _Model; set
            {
                _Model = value;
                RaisePropertyChanged();
            }
        }
        private INavigationService navigationService;
        private CancellationToken cancellationToken = new CancellationToken();
        private ObservableCollection<ChatResponse> _ChatList = new ObservableCollection<ChatResponse>();
        public ObservableCollection<ChatResponse> ChatList
        {
            get
            {
                return _ChatList;
            }
            set
            {
                _ChatList = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        public override Task<bool> OnAppearing(BaseView baseView)
        {
            Active = true;
            MessagingCenter.Subscribe<object, MessagesResponse>(this, "Chat", (sender, message) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (ChatList.Count > 0)
                    {
                        var chat = ChatList.Where(c => c.ChatId == message.ChatId).FirstOrDefault();
                        if (chat != null && Active)
                        {
                            chat.LastMessage = message.Content;
                        }
                    }
                });
            });
            return base.OnAppearing(baseView);
        }
        public override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<object, MessagesResponse>(this, "Chat");
            Active = false;
            base.OnDisappearing();
        }
        public override async Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    HasTabBar = !AppSharedPrefences.Instance.IsAutistic;
                    IsLoading = true;
                    LoadingText = "Loading...";
                    this.navigationService.UpdateBarBackgroundColor("#2965FF");
                    ChatList = new ObservableCollection<ChatResponse>(await Model.GetChats(cancellationToken));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsLoading = false;
                    LoadingText = "";
                }
            });


            return await base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        private CommandAsync _RefreshCommand = null;
        public CommandAsync RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = new CommandAsync(execute: async () =>
                    {
                        IsRefreshing = true;
                        ChatList = new ObservableCollection<ChatResponse>(await Model.GetChats(cancellationToken));
                        IsRefreshing = false;
                    });
                }
                return _RefreshCommand;
            }
        }
        private CommandAsync _ContactsCommand = null;
        public CommandAsync ContactsCommand
        {
            get
            {

                return _ContactsCommand ??
                    (_ContactsCommand = new CommandAsync(execute: async () =>
                    {
                        try
                        {
                            IsLoading = true;
                            await HideInflatorMenuCommand.ExecuteAsync();
                            await navigationService.NavigateTo<ContactsViewModel>(null);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                        finally
                        {
                            IsLoading = false;
                        }
                    }));
            }
        }
        private CommandAsync _SettingsCommand = null;
        public CommandAsync SettingsCommand
        {
            get
            {
                return _SettingsCommand ??

                    (_SettingsCommand = new CommandAsync(execute: async () =>
                    {
                        try
                        {
                            IsLoading = true;
                            await HideInflatorMenuCommand.ExecuteAsync();
                            await navigationService.NavigateTo<SettingsViewModel>(null);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                        finally
                        {
                            IsLoading = false;
                        }
                    }));
            }
        }
        private CommandAsync<object> _ChatClickedCommand = null;
        public CommandAsync<object> ChatClickedCommand
        {
            get
            {

                return _ChatClickedCommand ??
                    (_ChatClickedCommand = new CommandAsync<object>(async (member) =>
                    {
                        IsLoading = true;
                        IsLoading = true;
                        LoadingText = "Loading...";
                        var user = (member as ChatResponse);
                        var data = new Dictionary<string, object>();
                        data.Add("MemberInfo", user);
                        await navigationService.NavigateTo<ChatDetailsViewModel>(data);
                        IsLoading = false;
                    }));
            }
        }
        private CommandAsync _NewChatCommand = null;
        public CommandAsync NewChatCommand
        {
            get
            {

                return _NewChatCommand ??
                    (_NewChatCommand = new CommandAsync(execute: async () =>
                    {
                        IsLoading = true;
                        await navigationService.NavigateTo<ContactsViewModel>(null);
                        IsLoading = false;
                    }));
            }
        }
        #endregion
    }
}

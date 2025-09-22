using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Helpers;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using ConnectCareMobile.Services.APIServices.Response;
using ConnectCareMobile.ViewModels.Base;
using ConnectCareMobile.Views.Base;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    public class ChatDetailsViewModel : BaseViewModel
    {
        private INavigationService navigationService;
        public ChatDetailsViewModel(INavigationService navigationService)
        {
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            IsTitleView = false;
            IsProfileView = true;
            HasBackButton = true;
            HasNavBarOptionButton = true;
            HasNavBarVideoCallButton = true;
            HasNavBarVoiceCallButton = true;
            var color = ColorConverters.FromHex("#2965FF");
            StatusBar = new StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            Model = ViewModelLocator.Resolve<ChatDetailsModel>();
            this.navigationService = navigationService;
        }
        #region Properties
        private bool _Sending = false;
        public bool Sending
        {
            get
            {
                return _Sending;
            }
            set
            {
                _Sending = value;
                RaisePropertyChanged();
            }
        }
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
        private TextEntryModel _Message;
        public TextEntryModel Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
                RaisePropertyChanged();
            }
        }
        public void initializeInputs()
        {
            Message = new TextEntryModel
            {
                Placeholder = "Message",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
        }

        private CancellationToken cancellationToken = new CancellationToken();
        private ObservableCollection<MessagesResponse> _MessagesList;
        public ObservableCollection<MessagesResponse> MessagesList
        {
            get { return _MessagesList; }
            set
            {
                _MessagesList = value;
                RaisePropertyChanged();
            }
        }
        private ChatResponse _chatResponse;
        public ChatResponse chatResponse
        {
            get { return _chatResponse; }
            set
            {
                _chatResponse = value;
                RaisePropertyChanged();
            }
        }
        private ChatDetailsModel _Model;
        public ChatDetailsModel Model
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
        public override Task<bool> OnAppearing(BaseView baseView)
        {
            MessagingCenter.Subscribe<object, MessagesResponse>(this, "Chat", (sender, message) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (Active && chatResponse?.ChatId != null && chatResponse?.ChatId != Guid.Empty && chatResponse?.ChatId == message.ChatId)
                        MessagesList.Add(message);
                });
            });
            Active = true;
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
            try
            {
                initializeInputs();
                if (navigationData != null && navigationData.ContainsKey("MemberInfo"))
                {
                    chatResponse = navigationData["MemberInfo"] as ChatResponse;
                    if (chatResponse.SenderId != GlobalSettings.LoggedUser.UserId)
                    {
                        ChatReceiverName = $"{chatResponse.Sender.FirstName} {chatResponse.Sender.LastName}";
                    }
                    else
                    {
                        ChatReceiverName = $"{chatResponse.Receiver.FirstName} {chatResponse.Receiver.LastName}";
                    }
                    MessagesList = new ObservableCollection<MessagesResponse>(await Model.GetMessages(chatResponse.ChatId, GlobalSettings.LoggedUser.UserId, cancellationToken));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return await base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        private CommandAsync _SendMessageCommand;
        public CommandAsync SendMessageCommand
        {
            get
            {
                return _SendMessageCommand ??
                    (_SendMessageCommand = new CommandAsync(async () =>
                {
                    try
                    {
                        if (Sending)
                            return;
                        if (chatResponse != null && !string.IsNullOrEmpty(Message.Text))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                Sending = true;
                                await Model.SendMessage(chatResponse.ChatId, GlobalSettings.LoggedUser.UserId, Message.Text, MessageTypes.Message, cancellationToken);
                                MessagesList.Add(new MessagesResponse()
                                {
                                    UserId = GlobalSettings.LoggedUser.UserId,
                                    Content = Message.Text,
                                    ChatId = chatResponse.ChatId,
                                    CreatedAt = DateTime.Now,
                                    MessageType = MessageTypes.Message
                                });
                                Message.Text = "";
                                Sending = false;
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }));
            }
        }
        private CommandAsync _VoiceCallButtonCommand;
        public override CommandAsync VoiceCallButtonCommand
        {
            get
            {

                return _VoiceCallButtonCommand ??
                    (_VoiceCallButtonCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            var sinchService = DependencyService.Get<ISinchService>();
                            if (GlobalSettings.LoggedUser.UserId == chatResponse.ReceiverId)
                            {
                                sinchService.CallUser(chatResponse.SenderId.ToString());
                            }
                            else
                            {
                                sinchService.CallUser(chatResponse.ReceiverId.ToString());
                            }
                        }
                        catch (Exception ex) { }
                    }, () =>
                    {
                        return true;
                    }));
            }
        }
        #endregion

    }
}

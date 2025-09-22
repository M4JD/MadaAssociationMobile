using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    class ForgetPasswordViewModel : BaseViewModel
    {
        INavigationService navigationService = null;

        public ForgetPasswordViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        private TextEntryModel _Username;
        public TextEntryModel Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                RaisePropertyChanged();
            }
        }
        public override Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            initializeInputs();
            return Task.FromResult(true);
        }
        public void initializeInputs()
        {
            Username = new TextEntryModel(CheckCanSignIn)
            {
                Placeholder = "Username*",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
        }
        private Task CheckCanSignIn()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SendResetLinkCommand?.RaiseCanExecuteChanged();
            });
            return Task.CompletedTask;
        }
        private CommandAsync _SendResetLinkCommand = null;
        public CommandAsync SendResetLinkCommand
        {
            get
            {
                return _SendResetLinkCommand ?? (_SendResetLinkCommand = new CommandAsync(execute: async () =>
                 {
                     try
                     {
                         await navigationService.NavigateTo<LoginViewModel>(null, true);
                     }
                     catch (Exception ex)
                     {
                         HandleException(ex);
                     }
                 },
                 canExecute: () =>
                 {
                     return !string.IsNullOrEmpty(Username?.Text);
                 }));
            }
        }

    }
}

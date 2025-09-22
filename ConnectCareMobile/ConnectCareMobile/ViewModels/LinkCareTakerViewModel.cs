using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.ViewModels.Base;
using ConnectCareMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ConnectCareMobile.ViewModels
{
    class LinkCareTakerViewModel : BaseViewModel
    {
        INavigationService navigationService = null;

        public LinkCareTakerViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            initializeInputs();
            return Task.FromResult(true);
        }
        public void initializeInputs()
        {
            Username = new TextEntryModel
            {
                Placeholder = "Username*",
                IsPassword = false,
                ErrorText = "",
                IsError = false,
                Text = ""
            };
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
        private CommandAsync _CreateAccountCommand = null;
        public CommandAsync CreateAccountCommand
        {
            get
            {
                return _CreateAccountCommand ??
                     (_CreateAccountCommand = new CommandAsync(execute: async () =>
                     {
                         try
                         {
                             await navigationService.NavigateToView<CareTakerConfirmationView>(ignoreBinding: true);
                         }
                         catch (Exception ex)
                         {
                             HandleException(ex);
                         }
                     }));
            }
        }
    }
}

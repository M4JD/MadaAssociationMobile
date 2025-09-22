using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices.Params;
using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.ViewModels
{
    public class AccountTypeViewModel : BaseViewModel
    {
        INavigationService navigationService = null;
        public AccountTypeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        private CommandAsync _AutisticCreateAccountCommand;
        public CommandAsync AutisticCreateAccountCommand
        {
            get
            {

                return _AutisticCreateAccountCommand ??
                    (_AutisticCreateAccountCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            CreateUserParams UserData = new CreateUserParams();
                            UserData.IsAutistic = true;
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("UserData", UserData);
                            await navigationService.NavigateTo<PersonalInfoViewModel>(data);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    },
                    () => true
                    ));
            }
        }
        private CommandAsync _CreateAccountCommand;
        public CommandAsync CreateAccountCommand
        {
            get
            {

                return _CreateAccountCommand ??
                    (_CreateAccountCommand = new CommandAsync(async () =>
                    {
                        try
                        {
                            CreateUserParams UserData = new CreateUserParams();
                            UserData.IsAutistic = false;
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("UserData", UserData);
                            await navigationService.NavigateTo<PersonalInfoViewModel>(data);
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }));
            }
        }
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
                            await navigationService.NavigateBack();
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

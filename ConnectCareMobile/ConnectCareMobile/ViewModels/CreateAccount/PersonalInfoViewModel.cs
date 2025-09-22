using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Controls;
using ConnectCareMobile.Controls.Entries;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models;
using ConnectCareMobile.Services.APIServices.Params;
using ConnectCareMobile.ViewModels.Base;
using ConnectCareMobile.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConnectCareMobile.ViewModels
{
    internal class PersonalInfoViewModel : BaseViewModel
    {
        INavigationService navigationService = null;
        public PersonalInfoViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = ViewModelLocator.Resolve<PersonalInfoModel>();
        }
        private CreateUserParams UserData = null;
        #region Properties
        private string _MaxSteps = "Step 1 of 3: ";
        public string MaxSteps
        {
            get { return _MaxSteps; }
            set
            {
                _MaxSteps = value;
                RaisePropertyChanged();
            }
        }
        private PersonalInfoModel _Model;
        public PersonalInfoModel Model
        {
            get
            {
                return _Model;
            }
            set
            {
                _Model = value;
                NextCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
        private Selectable _Gender;
        public Selectable Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                _Gender = value;
                NextCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
        private Selectable _Country;
        public Selectable Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
                NextCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Functions
        private Task CheckCanContinue()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NextCommand?.RaiseCanExecuteChanged();
            });
            return Task.CompletedTask;
        }
        public override async Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                if (!navigationData.ContainsKey("UserData") || navigationData["UserData"] == null)
                {
                    Debug.WriteLine("Invalid params");
                    await navigationService.NavigateBack();
                    return await Task.FromResult(false);
                }
                else
                {
                    Model.initializeInputs(CheckCanContinue);
                    UserData = (CreateUserParams)navigationData["UserData"];
                    if (UserData.IsAutistic)
                    {
                        if (UserData.IsAutistic)
                        {
                            MaxSteps = "Step 1 of 4: ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return await Task.FromResult(true);
        }
        #endregion
        #region Commands
        private CommandAsync _NextCommand = null;
        public CommandAsync NextCommand
        {
            get
            {
                return _NextCommand ??
                    (_NextCommand = new CommandAsync(execute: async () =>
                    {
                        //TODO: Add user data to page payload
                        UserData.LastName = Model.LastName.Text;
                        UserData.FirstName = Model.FirstName.Text;
                        UserData.Gender = Gender.Value;
                        UserData.Country = Country.Value;
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("UserData", UserData);
                        await navigationService.NavigateTo<AccountInfoViewModel>(data);
                    },
                    canExecute: () =>
                    {
                        return Model.EntriesValid(Country, Gender);
                    }));
            }
        }
        private CommandAsync _GoBackCommand = null;
        public CommandAsync GoBackCommand
        {
            get
            {
                return _GoBackCommand ?? (_GoBackCommand = new CommandAsync(execute: async () =>
                    {
                        await navigationService.NavigateBack();
                    }));

            }
        }
        #endregion
    }
}

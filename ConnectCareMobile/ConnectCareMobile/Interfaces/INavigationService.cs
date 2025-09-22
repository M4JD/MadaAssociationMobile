using ConnectCareMobile.ViewModels;
using ConnectCareMobile.Views.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConnectCareMobile.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToView<TView>(Dictionary<string, object> parameter, bool removeFromStack = false) where TView : Page;
        Task NavigateTo<TViewModel>(Dictionary<string, object> parameter, bool removeFromStack = false, bool ignoreBinding = false) where TViewModel : BaseViewModel;
        Task NavigateToView<TView>(Dictionary<string, object> parameter = null, bool removeFromStack = false, bool ignoreBinding = true) where TView : BaseView;
        Task InitializeAsync();
        void UpdateBarBackgroundColor(string color);
        Task NavigateBack();
        Task NavigateToRoot();
    }
}

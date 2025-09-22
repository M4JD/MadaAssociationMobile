using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.ViewModels;
using ConnectCareMobile.Views;
using ConnectCareMobile.Views.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConnectCareMobile.Services.NavigationServices
{
    public class NavigationService : INavigationService
    {
        public static readonly NavigationPage navigatioStatic = new NavigationPage()
        {
            BarBackgroundColor = Color.Black,
            BackgroundImageSource = ImageSource.FromResource("BackArrow.png")
        };
        public void UpdateBarBackgroundColor(string color)
        {
            navigatioStatic.BarBackgroundColor = Color.FromHex(color);
        }
        private Page pageStatic;
        public async Task InitializeAsync()
        {
            Application.Current.MainPage = navigatioStatic;
            if (AppSharedPrefences.Instance.IsLoggedIn)
                if (AppSharedPrefences.Instance.IsAutistic)
                {
                    await NavigateTo<MessagesListViewModel>(null, false);
                }
                else
                {
                    await NavigateTo<MainViewModel>(null, false);
                }
            else
                await NavigateTo<LoginViewModel>(null, false);
            return;
        }
        public async Task NavigateToView<TView>(Dictionary<string, object> parameter = null, bool removeFromStack = false, bool ignoreBinding = false) where TView : BaseView
        {
            Page page = Activator.CreateInstance(typeof(TView)) as Page;
            if (!ignoreBinding)
            {
                if (!await (page.BindingContext as BaseViewModel).InitializeAsync(parameter))
                    return;
            }
            pageStatic = page;
            try
            {
                await navigatioStatic.PushAsync(page);
                if (removeFromStack)
                {
                    navigatioStatic.Navigation.RemovePage(navigatioStatic.Navigation.NavigationStack[navigatioStatic.Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task NavigateTo<TViewModel>(Dictionary<string, object> parameter, bool removeFromStack = false, bool ignoreBinding = false) where TViewModel : BaseViewModel
        {
            Page page = CreatePage(typeof(TViewModel));
            if (!ignoreBinding)
            {
                if (!await (page.BindingContext as BaseViewModel).InitializeAsync(parameter))
                    return;
            }
            pageStatic = page;
            try
            {
                await navigatioStatic.PushAsync(page, false);
                if (removeFromStack)
                {
                    navigatioStatic.Navigation.RemovePage(navigatioStatic.Navigation.NavigationStack[navigatioStatic.Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task NavigateToView<TView>(Dictionary<string, object> parameter, bool removeFromStack = false) where TView : Page
        {
            Page page = Activator.CreateInstance(typeof(TView)) as Page;
            try
            {
                await navigatioStatic.Navigation.PushModalAsync(page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task NavigateBack()
        {
            if (navigatioStatic.Navigation.NavigationStack.Count > 1)
            {
                await navigatioStatic.PopAsync(false);
                pageStatic = navigatioStatic.CurrentPage;
            }
        }
        public async Task NavigateToRoot()
        {
            await navigatioStatic.PopToRootAsync(false);
            pageStatic = navigatioStatic.CurrentPage;
        }

        #region Create page
        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeFromViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"cannot Locate page type for {viewModelType}");
            }
            else
            {
                try
                {
                    Page page = Activator.CreateInstance(pageType) as Page;
                    return page;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private Type GetPageTypeFromViewModel(Type viewModelType)
        {
            string viewName = viewModelType.FullName.Replace("Model", string.Empty);

            var viewModelAssembly = viewModelType.GetTypeInfo().Assembly.FullName;

            string viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssembly);

            Type viewType = Type.GetType(viewAssemblyName);

            return viewType;
        }
        #endregion
    }
}

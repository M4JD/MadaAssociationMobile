using ConnectCareMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConnectCareMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : TabbedPage
    {
        public MainView()
        {
            InitializeComponent();
        }
        protected override async void OnCurrentPageChanged()
        {
            var bindingContext = CurrentPage.BindingContext as BaseViewModel;
            if (bindingContext != null && bindingContext.IsLoading)
            {
                return;
            }
            else
            {
                await bindingContext.InitializeAsync(null);
            }
            base.OnCurrentPageChanged();
        }
    }
}
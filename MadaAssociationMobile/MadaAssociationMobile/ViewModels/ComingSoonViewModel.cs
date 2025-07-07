using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Services.NavigationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MadaAssociationMobile.ViewModels
{
    public class ComingSoonViewModel : BaseViewModel
    {
        public ComingSoonViewModel()
        {
            var color = ColorConverters.FromHex("#2965FF");
            NavBarBackGroundColor = Color.FromHex("#2965FF");
            HasNavBar = true;
            HasBackButton = false;
            HasNavBarOptionButton = false;
            StatusBar = new Base.StatusBar() { DarkStatusBarTint = false, StatusBarColor = color };
            HasTabBar = !AppSharedPrefences.Instance.IsAutistic;
        }
        public override Task<bool> InitializeAsync(Dictionary<string, object> navigationData)
        {
            try
            {
                if (navigationData != null && navigationData.ContainsKey("Title"))
                {
                    PageTitle = navigationData["Title"].ToString();
                    HasTabBar = false;
                }
                else
                {
                    PageTitle = "Shop";
                    HasTabBar = !AppSharedPrefences.Instance.IsAutistic;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return base.InitializeAsync(navigationData);
        }
    }
}

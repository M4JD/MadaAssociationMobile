using Android.OS;
using Android.Views;
using MadaAssociationMobile.Interfaces;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Provider.Settings;

[assembly: Dependency(typeof(MadaAssociationMobile.Droid.Helpers.DeviceInfo))]
namespace MadaAssociationMobile.Droid.Helpers
{
    public class DeviceInfo : IDeviceInfo
    {
        public string GetDeviceID()
        {
            string id = "";
            try
            {
                var context = Android.App.Application.Context;
                id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return id;
        }

        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBasTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;
            var activity = Platform.CurrentActivity;
            var window = activity.Window;
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(color.ToPlatformColor());

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.R)
            {
                if (darkStatusBasTint)
                {
                    window?.InsetsController?.SetSystemBarsAppearance((int)WindowInsetsControllerAppearance.LightStatusBars, (int)WindowInsetsControllerAppearance.LightStatusBars);
                }
                else { 
                    window?.InsetsController?.SetSystemBarsAppearance((int)WindowInsetsControllerAppearance.None, (int)WindowInsetsControllerAppearance.None);
                }
            }
            else
            {
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.M)
                {
                    var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                    window.DecorView.SystemUiVisibility = darkStatusBasTint ? flag : 0;
                }
            }

        }
    }
}
using Foundation;
using ConnectCareMobile.Interfaces;
using System;
using UIKit;
using Xamarin.Essentials;

namespace ConnectCareMobile.iOS.Helpers
{
    public class DeviceInfo : IDeviceInfo
    {
        public string GetDeviceID()
        {
            string id = "";
            try
            {
                id = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return id;
        }

        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBasTint)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                var statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                statusBar.BackgroundColor = color.ToPlatformColor();
                UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
            }
            else
            {
                var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    statusBar.BackgroundColor = color.ToPlatformColor();
                }
            }
            var style = darkStatusBasTint ? UIStatusBarStyle.DarkContent : UIStatusBarStyle.LightContent;
            UIApplication.SharedApplication.SetStatusBarStyle(style, false);
        }
    }
}
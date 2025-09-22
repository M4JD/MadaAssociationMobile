
using Firebase.Iid;
using ConnectCareMobile.Droid.Helpers;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.ViewModels.Base;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseMessagingHelper))]
namespace ConnectCareMobile.Droid.Helpers
{
    public class FirebaseMessagingHelper : IFirebaseMessagingHelper
    {
        public string GetFCMToken()
        {
            string FCMToken = "";
            try
            {
                FCMToken = FirebaseInstanceId.Instance.Token;
            }
            catch { }
            return FCMToken;
        }
    }
}

using Firebase.Iid;
using MadaAssociationMobile.Droid.Helpers;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.ViewModels.Base;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseMessagingHelper))]
namespace MadaAssociationMobile.Droid.Helpers
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
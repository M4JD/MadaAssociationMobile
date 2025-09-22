using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using ConnectCareMobile.Common.Global;

namespace ConnectCareMobile.Droid.Helpers
{
    [Service(Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            AppSharedPrefences.Instance.FCMToken = token;
            AppSharedPrefences.Instance.FCMTokenChanged = true;
            // Add custom implementation, as needed.
        }
    }
}
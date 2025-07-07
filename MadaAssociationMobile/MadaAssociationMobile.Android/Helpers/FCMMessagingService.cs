using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Firebase.Messaging;
using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Services.APIServices.Response;
using MadaAssociationMobile.ViewModels.Base;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using static Java.Util.Jar.Attributes;

namespace MadaAssociationMobile.Droid.Helpers
{
    [Service(Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FCMMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnNewToken(string token)
        {
            AppSharedPrefences.Instance.FCMToken = token;
            AppSharedPrefences.Instance.FCMTokenChanged = true;
            base.OnNewToken(token);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {

            var data = remoteMessage.Data;
            Log.Debug(TAG, "Got FCM message.");
            Log.Debug(TAG, "From: " + remoteMessage.From);
            Log.Debug(TAG, "Message: " + remoteMessage);
            Log.Debug(TAG, "Notification Message Body: " + remoteMessage.GetNotification().Body);
            Log.Debug(TAG, "Notification Message: " + remoteMessage.GetNotification());

            if (remoteMessage.Data.ContainsKey("Message"))
            {
                try
                {
                    var messageresponse = JsonConvert.DeserializeObject<MessagesResponse>(remoteMessage.Data["Message"]);
                    Xamarin.Forms.MessagingCenter.Send<object, MessagesResponse>(this, "Chat", messageresponse);
                }
                catch (Exception ex) { Crashes.TrackError(ex); }
            }
            return;
        }
    }
}
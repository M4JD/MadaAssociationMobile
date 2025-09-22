using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Interfaces
{
    public interface ISinchService
    {
        void InitializeChatSDK(string USER_IDENTIFIER);
        object RegisterUser(string USER_IDENTIFIER, string FCM_TOKEN);
        object SetConnectionListener();
        object CallUser(string USER_IDENTIFIER);
    }
}

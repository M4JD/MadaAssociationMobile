using ConnectCareMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Interfaces
{
    public interface IFirebaseMessagingHelper
    {
        string GetFCMToken();
    }
}

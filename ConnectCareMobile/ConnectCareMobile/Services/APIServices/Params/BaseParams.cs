using ConnectCareMobile.Common.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class BaseParams
    {
        public string FCM { get; set; } = GlobalSettings.FCM;
        public string OS { get; set; } = GlobalSettings.OS;
        public string UDID { get; set; } = GlobalSettings.UDID;
    }
}

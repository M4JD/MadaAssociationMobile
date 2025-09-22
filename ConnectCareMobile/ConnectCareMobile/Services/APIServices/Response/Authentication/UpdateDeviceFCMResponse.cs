using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices
{
    public class UpdateDeviceFCMResponse
    {
        public Guid DeviceId { get; set; }
        public string FCM { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class VerifyPhoneNumberParams : BaseParams
    {
        public string Username { get; set; }
        public string OTP { get; set; }
    }
}

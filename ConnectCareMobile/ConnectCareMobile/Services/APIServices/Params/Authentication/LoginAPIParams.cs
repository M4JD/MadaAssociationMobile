using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class LoginAPIParams : BaseParams
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
}

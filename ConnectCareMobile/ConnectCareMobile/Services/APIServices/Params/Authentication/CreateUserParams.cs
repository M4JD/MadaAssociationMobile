using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class CreateUserParams : BaseParams
    {
        public string Gender { get; set; } = "";
        public string Country { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string Username { get; set; } = "";
        public string CareTakerUsername { get; set; } = "";
        public string CareTakerRelation { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string PhoneCountryCode { get; set; } = "";
        public string PhoneNumericCountryCode { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsAutistic { get; set; } = false;
    }
}

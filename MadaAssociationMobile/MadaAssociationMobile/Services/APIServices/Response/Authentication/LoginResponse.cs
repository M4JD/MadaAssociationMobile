using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices
{
    public class LoginResponse
    {
        public string Gender { get; set; }
        public Guid MemberId { get; set; }
        public Guid UserId { get; set; }
        public Guid DeviceId { get; set; }
        public string Country { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string CareTakerUsername { get; set; }
        public string CareTakerRelation { get; set; } = "";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhoneCountryCode { get; set; }
        public bool IsAutistic { get; set; }
        public string Token { get; set; }
        public string PhoneNumericCountryCode { get; set; }
    }
}

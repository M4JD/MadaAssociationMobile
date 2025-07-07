using MadaAssociationMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices
{
    public class MemberResponse
    {
        public Guid MemberId { get; set; }
        public Guid ConnectionId { get; set; }
        public Guid UserId { get; set; }
        public string Phone { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumericCountryCode { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string SupervisorRelation { get; set; } = "";
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public string DisplayName { get; set; } = "";
        public bool IsSupervisor { get; set; } = false;
        //used to check if supervisor accepted the request of the user to be under his care
        public bool IsApproved { get; set; } = false;
        //used for connection request
        public bool IsAccepted { get; set; } = false;
    }
}

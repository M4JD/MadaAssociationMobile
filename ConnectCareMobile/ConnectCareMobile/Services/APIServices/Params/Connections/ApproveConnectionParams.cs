using System;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class ApproveConnectionParams : BaseParams
    {
        public Guid UserId { get; set; }
        public Guid ConnectionId { get; set; }
    }
}

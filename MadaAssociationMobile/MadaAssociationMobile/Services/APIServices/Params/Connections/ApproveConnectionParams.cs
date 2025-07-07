using System;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class ApproveConnectionParams : BaseParams
    {
        public Guid UserId { get; set; }
        public Guid ConnectionId { get; set; }
    }
}

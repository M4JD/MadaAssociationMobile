using System;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class ApproveRequestParams : BaseParams
    {
        public Guid SupervisorId { get; set; }
        public Guid MemberId { get; set; }
    }
}

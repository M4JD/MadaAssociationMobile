using System;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class ApproveRequestParams : BaseParams
    {
        public Guid SupervisorId { get; set; }
        public Guid MemberId { get; set; }
    }
}

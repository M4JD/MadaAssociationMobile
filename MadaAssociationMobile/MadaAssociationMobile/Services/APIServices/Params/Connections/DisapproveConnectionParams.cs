using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params.Connections
{
    public class DisapproveConnectionParams : BaseParams
    {
        public Guid UserId { get; set; }
        public Guid ConnectionId { get; set; }
    }
}

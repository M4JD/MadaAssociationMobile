using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class GetAutisticUserConnectionsParams
    {
        public Guid UserId { get; set; }
        public Guid SupervisorId { get; set; }
    }
}

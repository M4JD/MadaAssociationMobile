using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class GetConnectionsParams : BaseParams
    {
        public Guid UserId { get; set; }
    }
}

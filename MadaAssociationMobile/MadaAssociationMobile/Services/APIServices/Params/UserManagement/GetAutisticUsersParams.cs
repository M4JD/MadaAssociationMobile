using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class GetAutisticUsersParams : BaseParams
    {
        public Guid SupervisorId { get; set; }
    }
}

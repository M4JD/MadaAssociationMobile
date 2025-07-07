using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class GetChatsParams : BaseParams
    {
        public Guid UserId { get; set; }
    }
}

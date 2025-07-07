using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class GetMessagesParams : BaseParams
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class AddChatParams : BaseParams
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}

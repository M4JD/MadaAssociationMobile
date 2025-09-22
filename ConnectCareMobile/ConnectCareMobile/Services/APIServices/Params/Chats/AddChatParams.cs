using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class AddChatParams : BaseParams
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}

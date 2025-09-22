using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class SendMessageParams : BaseParams
    {
        public string Content { get; set; }
        public string MessageType { get; set; }
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
    }
}

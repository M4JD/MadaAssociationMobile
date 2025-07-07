using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices
{
    public class ChatResponse
    {
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public MemberResponse Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public MemberResponse Receiver { get; set; }
        public string LastMessage { get; set; }
    }
}

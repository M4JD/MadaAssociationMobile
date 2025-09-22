using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Response
{
    public class MessagesResponse
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string MessageType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}

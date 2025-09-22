using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class GetMessagesParams : BaseParams
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Services.APIServices.Params
{
    public class GetChatsParams : BaseParams
    {
        public Guid UserId { get; set; }
    }
}

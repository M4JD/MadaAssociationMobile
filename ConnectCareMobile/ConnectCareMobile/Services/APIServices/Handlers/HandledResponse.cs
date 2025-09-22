using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConnectCareMobile.Services.APIServices
{
    public class HandledResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Response { get; set; }
        public static HandledResponse<T> EmptySuccessResponse() => new HandledResponse<T>
        {
            Message = string.Empty,
            Status = true,
            Response = default(T)
        };
    }
}

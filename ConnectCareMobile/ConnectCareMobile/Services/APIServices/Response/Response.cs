using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConnectCareMobile.Services.APIServices
{
    public class Response<T>
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public T Results { get; set; }
    }
}

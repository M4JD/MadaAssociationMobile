using Flurl.Http.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace  MadaAssociationMobile.APIService.HttpWrappers
{
    public class ProxyHttpClientFactory : DefaultHttpClientFactory
    {
        private string _address;

        public ProxyHttpClientFactory(string address)
        {
            _address = address;
        }

        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                Proxy = new WebProxy(_address),
                UseProxy = true
            };
        }
    }
}

using ConnectCareMobile.APIService.HttpWrappers;
using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using ConnectCareMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.APIServices
{
    public class MessagesAPIService : HttpWrapper, IMessagesAPIService
    {
        private ISettings iSettings;
        public MessagesAPIService(ISettings iSettings) : base(iSettings)
        {
            iSettings.APIURL = GlobalSettings.APIURL;
            this.iSettings = iSettings;
        }

        public async Task<Response<SendMessageResponse>> SendMessage(SendMessageParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<SendMessageResponse>>("", "", body, "Messages/SendMessage", cancellationToken);
            return result;
        }

        public async Task<Response<GetMessagesResponse>> GetMessages(GetMessagesParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<GetMessagesResponse>>("", "", body, "Messages/GetMessages", cancellationToken);
            return result;
        }
    }
}

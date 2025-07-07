using MadaAssociationMobile.APIService.HttpWrappers;
using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Params;
using MadaAssociationMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.APIServices
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

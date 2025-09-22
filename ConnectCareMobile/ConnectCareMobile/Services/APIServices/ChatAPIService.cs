using ConnectCareMobile.APIService.HttpWrappers;
using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.APIServices
{
    public class ChatAPIService : HttpWrapper, IChatAPIService
    {
        private ISettings iSettings;
        public ChatAPIService(ISettings iSettings) : base(iSettings)
        {
            iSettings.APIURL = GlobalSettings.APIURL;
            this.iSettings = iSettings;
        }

        public async Task<Response<AddChatResponse>> AddChat(AddChatParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<AddChatResponse>>("", "", body, "Chat/AddChat", cancellationToken);
            return result;
        }

        public async Task<Response<DeleteChatsResponse>> DeleteChats(DeleteChatsParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<DeleteChatsResponse>>("", "", body, "Chat/DeleteChats", cancellationToken);
            return result;
        }

        public async Task<Response<GetChatsResponse>> GetChats(GetChatsParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<GetChatsResponse>>("", "", body, "Chat/GetChats", cancellationToken);
            return result;
        }
    }
}

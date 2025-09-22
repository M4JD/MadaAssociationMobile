using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using ConnectCareMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.Interfaces
{
    public interface IMessagesAPIService
    {
        Task<Response<SendMessageResponse>> SendMessage(SendMessageParams body, CancellationToken cancellationToken);
        Task<Response<GetMessagesResponse>> GetMessages(GetMessagesParams body, CancellationToken cancellationToken);
    }
}

using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Params;
using MadaAssociationMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Interfaces
{
    public interface IMessagesAPIService
    {
        Task<Response<SendMessageResponse>> SendMessage(SendMessageParams body, CancellationToken cancellationToken);
        Task<Response<GetMessagesResponse>> GetMessages(GetMessagesParams body, CancellationToken cancellationToken);
    }
}

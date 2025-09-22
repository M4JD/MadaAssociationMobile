using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.Interfaces
{
    public interface IChatAPIService
    {
        Task<Response<GetChatsResponse>> GetChats(GetChatsParams body, CancellationToken cancellationToken);
        Task<Response<AddChatResponse>> AddChat(AddChatParams body, CancellationToken cancellationToken);
        Task<Response<DeleteChatsResponse>> DeleteChats(DeleteChatsParams body, CancellationToken cancellationToken);
    }
}

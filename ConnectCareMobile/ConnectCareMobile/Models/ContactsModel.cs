using ConnectCareMobile.APIServices;
using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models.Base;
using ConnectCareMobile.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.Models
{
    public class ContactsModel : BaseModel
    {
        private IChatAPIService _chatAPIService;
        private IConnectionsAPIService _connectionsAPIService;
        private CancellationToken _cancellationToken = new CancellationToken();
        public ContactsModel(
            IConnectionsAPIService connectionsAPIService,
            IChatAPIService chatAPIService)
        {
            _connectionsAPIService = connectionsAPIService;
            _chatAPIService = chatAPIService;
        }
        public async Task<bool> SendConnectionRequest(string friendUsername)
        {
            var result = await _connectionsAPIService.CreateConnection(new Services.APIServices.Params.CreateConnectionParams() { FriendUserName = friendUsername, UserId = GlobalSettings.LoggedUser.UserId }, _cancellationToken);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                return result.Results.isCreated;
            }
            return false;
        }
        public async Task<List<MemberResponse>> GetConnections(Guid userId)
        {
            var result = await _connectionsAPIService.GetConnections(new Services.APIServices.Params.GetConnectionsParams() { UserId = GlobalSettings.LoggedUser.UserId }, _cancellationToken);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                return result.Results?.Members;
            }
            else
            {
                return new List<MemberResponse>();
            }
        }
        public async Task<bool> AcceptConnection(Guid ConnectionId)
        {
            var result = await _connectionsAPIService.AcceptConnection(new Services.APIServices.Params.ApproveConnectionParams()
            {
                ConnectionId = ConnectionId,
                UserId = GlobalSettings.LoggedUser.UserId

            }, _cancellationToken);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                return result.Results.Approved;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> RejectConnection(Guid ConnectionId)
        {
            var result = await _connectionsAPIService.RejectConnection(new Services.APIServices.Params.Connections.DisapproveConnectionParams()
            {
                UserId = GlobalSettings.LoggedUser.UserId,
                ConnectionId = ConnectionId
            }, _cancellationToken);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                return result.Results.Disapproved;
            }
            else
            {
                return false;
            }
        }
        public async Task<Guid> AddChat(Guid userId)
        {
            var result = await _chatAPIService.AddChat(new Services.APIServices.Params.AddChatParams() { SenderId = GlobalSettings.LoggedUser.UserId, ReceiverId = userId }, _cancellationToken);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                if (result.Results != null)
                {
                    return result.Results.ChatId;

                }
                else
                {
                    return Guid.Empty;
                }
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}

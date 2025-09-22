using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models.Base;
using ConnectCareMobile.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.Models
{
    public class UserFriendsModel : BaseModel
    {
        private readonly IConnectionsAPIService _connectionsAPIService;
        public UserFriendsModel(IConnectionsAPIService connectionsAPIService)
        {
            _connectionsAPIService = connectionsAPIService;
        }
        #region Properties

        #endregion
        #region Functions
        public async Task<List<MemberResponse>> GetAutisticUserConnections(Guid AutUserId, CancellationToken cancellationToken)
        {
            var response = await _connectionsAPIService.GetAutisticUserConnections(new Services.APIServices.Params.GetAutisticUserConnectionsParams()
            {
                SupervisorId = GlobalSettings.LoggedUser.UserId,
                UserId = AutUserId
            }, cancellationToken);
            if (response != null && response.Status == System.Net.HttpStatusCode.OK)
            {
                return response.Results.Members;
            }
            else
            {
                return new List<MemberResponse>();
            }
        }
        public async Task<bool> ApproveConnection(Guid ConnectionId, CancellationToken cancellationToken)
        {
            var response = await _connectionsAPIService.ApproveConnection(new Services.APIServices.Params.ApproveConnectionParams()
            {
                ConnectionId = ConnectionId,
                UserId = GlobalSettings.LoggedUser.UserId
            }, cancellationToken);

            if (response.Status == HttpStatusCode.OK && response.Results != null)
            {
                return response.Results.Approved;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DisapproveConnection(Guid ConnectionId, CancellationToken cancellationToken)
        {
            var response = await _connectionsAPIService.DisapproveConnection(new Services.APIServices.Params.Connections.DisapproveConnectionParams()
            {
                ConnectionId = ConnectionId,
                UserId = GlobalSettings.LoggedUser.UserId
            }, cancellationToken);

            if (response.Status == HttpStatusCode.OK && response.Results != null)
            {
                return response.Results.Disapproved;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

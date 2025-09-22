using ConnectCareMobile.APIService.HttpWrappers;
using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using ConnectCareMobile.Services.APIServices.Params.Connections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.APIServices
{
    public class ConnectionsAPIService : HttpWrapper, IConnectionsAPIService
    {
        private ISettings iSettings;
        public ConnectionsAPIService(ISettings iSettings) : base(iSettings)
        {
            iSettings.APIURL = GlobalSettings.APIURL;
            this.iSettings = iSettings;
        }
        #region Common
        public async Task<Response<GetConnectionsResponse>> GetConnections(GetConnectionsParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<GetConnectionsResponse>>("", "", body, "Connections/GetConnections", cancellationToken);
            return result;
        }
        public async Task<Response<CreateConnectionResponse>> CreateConnection(CreateConnectionParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<CreateConnectionResponse>>("", "", body, "Connections/CreateConnection", cancellationToken);
            return result;
        }
        public async Task<Response<ApproveConnectionResponse>> AcceptConnection(ApproveConnectionParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<ApproveConnectionResponse>>("", "", body, "Connections/AcceptConnection", cancellationToken);
            return result;
        }

        public async Task<Response<DisapproveConnectionResponse>> RejectConnection(DisapproveConnectionParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<DisapproveConnectionResponse>>("", "", body, "Connections/RejectConnection", cancellationToken);
            return result;
        }
        #endregion
        #region Caregiver
        public async Task<Response<ApproveConnectionResponse>> ApproveConnection(ApproveConnectionParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<ApproveConnectionResponse>>("", "", body, "Connections/ApproveConnection", cancellationToken);
            return result;
        }
        public async Task<Response<GetAutisticUserConnectionsResponse>> GetAutisticUserConnections(GetAutisticUserConnectionsParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<GetAutisticUserConnectionsResponse>>("", "", body, "Connections/GetAutisticUserConnections", cancellationToken);
            return result;
        }
        public async Task<Response<DisapproveConnectionResponse>> DisapproveConnection(DisapproveConnectionParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<DisapproveConnectionResponse>>("", "", body, "Connections/DisapproveConnection", cancellationToken);
            return result;
        }
        #endregion





    }
}

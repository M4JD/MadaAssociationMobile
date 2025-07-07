using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Params;
using MadaAssociationMobile.Services.APIServices.Params.Connections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Interfaces
{
    public interface IConnectionsAPIService
    {
        Task<Response<GetConnectionsResponse>> GetConnections(GetConnectionsParams body, CancellationToken cancellationToken);
        Task<Response<CreateConnectionResponse>> CreateConnection(CreateConnectionParams body, CancellationToken cancellationToken);
        Task<Response<ApproveConnectionResponse>> ApproveConnection(ApproveConnectionParams body, CancellationToken cancellationToken);
        Task<Response<GetAutisticUserConnectionsResponse>> GetAutisticUserConnections(GetAutisticUserConnectionsParams body, CancellationToken cancellationToken);
        Task<Response<DisapproveConnectionResponse>> DisapproveConnection(DisapproveConnectionParams body, CancellationToken cancellationToken);
        Task<Response<ApproveConnectionResponse>> AcceptConnection(ApproveConnectionParams body, CancellationToken cancellationToken);
        Task<Response<DisapproveConnectionResponse>> RejectConnection(DisapproveConnectionParams body, CancellationToken cancellationToken);
    }
}

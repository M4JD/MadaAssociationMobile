using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.Services.APIServices.Params;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Interfaces
{
    public interface IUserManagementAPIService
    {
        Task<Response<GetAutisticUsersResponse>> GetAutisticUsers(GetAutisticUsersParams body, CancellationToken cancellationToken);
        Task<Response<RejectRequestResponse>> RejectRequest(RejectRequestParams body, CancellationToken cancellationToken);
        Task<Response<ApproveRequestResponse>> ApproveRequest(ApproveRequestParams body, CancellationToken cancellationToken);
    }
}

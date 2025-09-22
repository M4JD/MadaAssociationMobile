using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.Interfaces
{
    public interface IUserManagementAPIService
    {
        Task<Response<GetAutisticUsersResponse>> GetAutisticUsers(GetAutisticUsersParams body, CancellationToken cancellationToken);
        Task<Response<RejectRequestResponse>> RejectRequest(RejectRequestParams body, CancellationToken cancellationToken);
        Task<Response<ApproveRequestResponse>> ApproveRequest(ApproveRequestParams body, CancellationToken cancellationToken);
    }
}

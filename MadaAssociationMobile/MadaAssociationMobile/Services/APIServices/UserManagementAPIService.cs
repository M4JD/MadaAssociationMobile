using MadaAssociationMobile.APIService.HttpWrappers;
using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Services.APIServices.Params;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Services.APIServices
{
    public class UserManagementAPIService : HttpWrapper, IUserManagementAPIService
    {
        private ISettings iSettings;
        public UserManagementAPIService(ISettings iSettings) : base(iSettings)
        {
            this.iSettings = iSettings;
            iSettings.APIURL = GlobalSettings.APIURL;
        }

        public async Task<Response<ApproveRequestResponse>> ApproveRequest(ApproveRequestParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<ApproveRequestResponse>>("", "", body, "UserManagement/ApproveRequest", cancellationToken);
            return result;
        }

        public async Task<Response<GetAutisticUsersResponse>> GetAutisticUsers(GetAutisticUsersParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<GetAutisticUsersResponse>>("", "", body, "UserManagement/GetAutisticUsers", cancellationToken);
            return result;
        }

        public async Task<Response<RejectRequestResponse>> RejectRequest(RejectRequestParams body, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<RejectRequestResponse>>("", "", body, "UserManagement/RejectRequest", cancellationToken);
            return result;
        }
    }
}

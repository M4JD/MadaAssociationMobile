using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models.Base;
using MadaAssociationMobile.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Models
{
    public class UserManagementModel : BaseModel
    {
        private IUserManagementAPIService _userManagementAPI;

        public UserManagementModel(IUserManagementAPIService userManagementAPI)
        {
            _userManagementAPI = userManagementAPI;
        }
        #region Functions
        public async Task<List<MemberResponse>> GetAutisticUsers(CancellationToken cancellationToken)
        {
            var response = await _userManagementAPI.GetAutisticUsers(new Services.APIServices.Params.GetAutisticUsersParams() { SupervisorId = GlobalSettings.LoggedUser.MemberId }, cancellationToken);

            if (response.Status == HttpStatusCode.OK && response.Results != null)
            {
                return response.Results.Members;
            }
            else
            {
                return new List<MemberResponse>();
            }
        }
        public async Task<bool> ApproveRequest(Guid MemberId, CancellationToken cancellationToken)
        {
            var response = await _userManagementAPI.ApproveRequest(new Services.APIServices.Params.ApproveRequestParams() { SupervisorId = GlobalSettings.LoggedUser.MemberId, MemberId = MemberId }, cancellationToken);

            if (response.Status == HttpStatusCode.OK && response.Results != null)
            {
                return response.Results.Approved;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> RejectRequest(Guid MemberId, CancellationToken cancellationToken)
        {
            var response = await _userManagementAPI.RejectRequest(new Services.APIServices.Params.RejectRequestParams() { SupervisorId = GlobalSettings.LoggedUser.MemberId, MemberId = MemberId }, cancellationToken);

            if (response.Status == HttpStatusCode.OK && response.Results != null)
            {
                return response.Results.Rejected;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

using MadaAssociationMobile.Services.APIServices.Params;
using MadaAssociationMobile.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MadaAssociationMobile.Interfaces
{
    public interface IUserAPIService
    {
        Task<Response<UpdateDeviceFCMResponse>> UpdateDeviceFCM(UpdateDeviceFCMParams updateDeviceFCMParams, CancellationToken cancellationToken);
        Task<Response<VerifyPhoneNumberResponse>> VerifyPhoneNumber(VerifyPhoneNumberParams verifyPhoneNumberParams, CancellationToken cancellationToken);
        Task<Response<CreateAccountResponse>> CreateAccount(CreateUserParams createUserParams, CancellationToken cancellationToken);
        Task<Response<CreateAccountResponse>> CreateAutisticAccount(CreateUserParams createUserParams, CancellationToken cancellationToken);
        Task<Response<CreateAccountResponse>> UpdateAutisticAccountCareTaker(UpdateAutisticAccountCareTakerParams createUserParams, CancellationToken cancellationToken);
        Task<Response<ForgetPasswordResponse>> ForgetPassword(ForgetPasswordAPIParams forgetPasswordAPIParams, CancellationToken cancellationToken);
        Task<Response<LoginResponse>> Login(LoginAPIParams loginAPIParams, CancellationToken cancellationToken);
    }
}

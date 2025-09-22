using ConnectCareMobile.APIService.HttpWrappers;
using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Common.Global.Enum;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Services.APIServices;
using ConnectCareMobile.Services.APIServices.Params;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.APIServices
{
    public class UserAPIService : HttpWrapper, IUserAPIService
    {
        private ISettings iSettings;
        public UserAPIService(ISettings iSettings) : base(iSettings)
        {
            iSettings.APIURL = GlobalSettings.APIURL;
            this.iSettings = iSettings;
        }
        //public async Task<Response> MobileForceUpdate(MobileForceUpdateParams forceUpdateParams)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPICallAsync<Response>("", "", forceUpdateParams, "api/Authentication/MobileForceUpdate");
        //    return result;
        //}
        //public async Task<PublicTokenResult> GetToken(GetTokenAPIParam getTokenAPIParam)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPITokenCallAsync<PublicTokenResult>("", "", getTokenAPIParam, "connect/token");
        //    return result;
        //}
        public async Task<Response<CreateAccountResponse>> CreateAutisticAccount(CreateUserParams createUserParams, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<CreateAccountResponse>>("", "", createUserParams, "Authentication/CreateAutisticAccount", cancellationToken);
            return result;
        }
        public async Task<Response<CreateAccountResponse>> CreateAccount(CreateUserParams createUserParams, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<CreateAccountResponse>>("", "", createUserParams, "Authentication/CreateAccount", cancellationToken);
            return result;
        }
        public async Task<Response<ForgetPasswordResponse>> ForgetPassword(ForgetPasswordAPIParams forgetPasswordAPIParams, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<ForgetPasswordResponse>>("", "", forgetPasswordAPIParams, "Authentication/ForgetPassword", cancellationToken);
            return result;
        }
        public async Task<Response<LoginResponse>> Login(LoginAPIParams loginAPIParams, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<LoginResponse>>("", "", loginAPIParams, "Authentication/Login");
            return result;
        }

        public async Task<Response<VerifyPhoneNumberResponse>> VerifyPhoneNumber(VerifyPhoneNumberParams verifyPhoneNumberParams, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<VerifyPhoneNumberResponse>>("", "", verifyPhoneNumberParams, "Authentication/VerifyPhoneNumber", cancellationToken);
            return result;
        }

        public async Task<Response<CreateAccountResponse>> UpdateAutisticAccountCareTaker(UpdateAutisticAccountCareTakerParams createUserParams, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<CreateAccountResponse>>("", "", createUserParams, "Authentication/UpdateAutisticAccountCareTaker", cancellationToken);
            return result;
        }

        public async Task<Response<UpdateDeviceFCMResponse>> UpdateDeviceFCM(UpdateDeviceFCMParams updateDeviceFCMParams, CancellationToken cancellationToken)
        {
            var result = await PostAPICallAsync<Response<UpdateDeviceFCMResponse>>("", "", updateDeviceFCMParams, "Authentication/UpdateDeviceFCM", cancellationToken);
            return result;
        }
        //public async Task<Response> ForgotPassword(ForgotPasswordAPIParam forgotPasswordAPIParam, CancellationToken cancellationToken)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPICallAsync<Response>("", "", forgotPasswordAPIParam, "api/Authentication/forgotpassword", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> ConfirmEmail(ConfirmEmailAPIParam confirmEmailAPIParam, CancellationToken cancellationToken)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPICallAsync<Response>("", "", confirmEmailAPIParam, "api/Authentication/confirmemail", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> VerifyUser(UserValidationAPIParam userValidationAPIParam, CancellationToken cancellationToken)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPICallAsync<Response>("", "", userValidationAPIParam, "api/Authentication/uservalidation", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> VerifyCode(CodeValidationAPIParam codeValidationAPIParam, CancellationToken cancellationToken)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPICallAsync<Response>("", "", codeValidationAPIParam, "api/Authentication/verifyuser", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> ResendVerificationCode(UserValidationAPIParam userValidationAPIParam, CancellationToken cancellationToken)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPICallAsync<Response>("", "", userValidationAPIParam, "api/Authentication/resendverificationcode", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> ResendConfirmationEmail(EmptyAPIParam emptyAPIParam, CancellationToken cancellationToken)
        //{
        //    iSettings.APIToken = AppSharedPrefences.Instance.APIPublicToken;
        //    var result = await PostAPICallAsync<Response>("", "", emptyAPIParam, "api/Authentication/resendconfirmemail", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> GetUserProfile(EmptyAPIParam emptyAPIParam)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new UserProfileResult()) };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new UserProfileResult()) };

        //    var result = await PostAPICallAsync<Response>("", "", emptyAPIParam, "api/Profile/getUserProfile", default);
        //    return result;
        //}
        //public async Task<Response> GetUserDependents(GetDependentParam getDependentParam)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new UserDependentResult()) };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new UserDependentResult()) };

        //    var result = await PostAPICallAsync<Response>("", "", getDependentParam, "api/Dependent/get", default);
        //    return result;
        //}
        //public async Task<Response> UploadProfilePic(Stream imageStream, string fileName, string mime)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed };

        //    var result = await PostDocumentAPICallAsync<Response>("", "", imageStream, "api/Profile/uploadImage", fileName, mime, default);
        //    return result;
        //}
        //public async Task<Response> RemoveProfilePic(EmptyAPIParam emptyAPIParam)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed };

        //    var result = await PostAPICallAsync<Response>("", "", emptyAPIParam, "api/Profile/deleteProfileImage", default);
        //    return result;
        //}
        //public async Task<Response> EditPhoneNumber(EditPhoneNumberParam editPhoneNumberParam, CancellationToken cancellationToken)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new UserRegistrationResult()) };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new UserRegistrationResult()) };

        //    var result = await PostAPICallAsync<Response>("", "", editPhoneNumberParam, "api/Profile/addPhoneNumber", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> VerifyCodeChangePhoneNumber(CodeValidationChangeNumberAPIParam codeValidationChangeNumberAPIParam, CancellationToken cancellationToken)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new VerifyCodeChangePhoneNumberResult()) };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new VerifyCodeChangePhoneNumberResult()) };

        //    var result = await PostAPICallAsync<Response>("", "", codeValidationChangeNumberAPIParam, "api/Profile/verifyPhoneNumber", cancellationToken);
        //    return result;
        //}
        //public async Task<Response> GetRelationshipTypes(EmptyAPIParam emptyAPIParam)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new RelationshipTypeResult()) };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new RelationshipTypeResult()) };

        //    var result = await PostAPICallAsync<Response>("", "", emptyAPIParam, "api/Profile/getRelationTypes", default);
        //    return result;
        //}
        //public async Task<Response> SaveDependent(SaveDepedentAPIParam saveDepedentAPIParam)
        //{
        //    if (AppSharedPrefences.Instance.IsLoggedIn == true)
        //        iSettings.APIToken = AppSharedPrefences.Instance.APIPrivateToken;
        //    else
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new RelationshipTypeResult()) };

        //    if (string.IsNullOrWhiteSpace(iSettings.APIToken))
        //        return new Response { Status = APIResponseStatuses.Failed, Result = JsonConvert.SerializeObject(new RelationshipTypeResult()) };

        //    var result = await PostAPICallAsync<Response>("", "", saveDepedentAPIParam, "api/Dependent/add", default);
        //    return result;
        //}
    }
}

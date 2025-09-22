using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConnectCareMobile.Services.APIServices
{
    public static class AuthenticationAPIHandler
    {
        public static HandledResponse<LoginResponse> HandleLoginAPICall(this Response<LoginResponse> response)
        {
            var Result = HandledResponse<LoginResponse>.EmptySuccessResponse();
            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    Result.Status = true;
                    Result.Response = response.Results;
                    break;
                case HttpStatusCode status when (status == HttpStatusCode.InternalServerError || status == HttpStatusCode.Gone || status == HttpStatusCode.Unauthorized || status == HttpStatusCode.BadRequest || status == HttpStatusCode.Unused):
                    Result.Status = false;
                    Result.Message = response.Message;
                    Result.Response = default(LoginResponse);
                    break;
                default:
                    Result.Status = false;
                    break;
            }
            return Result;
        }
        public static HandledResponse<CreateAccountResponse> HandleCreateAccountAPICall(this Response<CreateAccountResponse> response)
        {
            var Result = HandledResponse<CreateAccountResponse>.EmptySuccessResponse();
            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    Result.Status = true;
                    Result.Response = response.Results;
                    break;
                case HttpStatusCode.InternalServerError | HttpStatusCode.BadRequest:
                    Result.Status = false;
                    Result.Message = response.Message;
                    Result.Response = default(CreateAccountResponse);
                    break;
                default:
                    Result.Status = false;
                    break;
            }
            return Result;
        }
        public static HandledResponse<VerifyPhoneNumberResponse> HandleVerifyPhoneNumberAPICall(this Response<VerifyPhoneNumberResponse> response)
        {
            var Result = HandledResponse<VerifyPhoneNumberResponse>.EmptySuccessResponse();
            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    Result.Status = true;
                    Result.Response = response.Results;
                    break;
                case HttpStatusCode.InternalServerError | HttpStatusCode.BadRequest | HttpStatusCode.Unauthorized:
                    Result.Status = false;
                    Result.Message = response.Message;
                    Result.Response = default(VerifyPhoneNumberResponse);
                    break;
                default:
                    Result.Status = false;
                    break;
            }
            return Result;
        }
    }
}

using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models.Base;
using MadaAssociationMobile.Services.APIServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Models
{
    public class MessagesListModel : BaseModel
    {
        private IChatAPIService _chatAPIService;

        public MessagesListModel(IChatAPIService chatAPIService)
        {
            _chatAPIService = chatAPIService;
        }

        #region Frunctions
        public async Task<List<ChatResponse>> GetChats(CancellationToken cancellationToken)
        {
            List<ChatResponse> results = new List<ChatResponse>();
            if (GlobalSettings.LoggedUser == null)
            {
                if (!string.IsNullOrEmpty(AppSharedPrefences.Instance.LoggedInUser))
                {
                    GlobalSettings.LoggedUser = JsonConvert.DeserializeObject<LoginResponse>(AppSharedPrefences.Instance.LoggedInUser);
                    var response = await _chatAPIService.GetChats(new Services.APIServices.Params.GetChatsParams() { UserId = GlobalSettings.LoggedUser.UserId }, cancellationToken);
                    if (response.Status == System.Net.HttpStatusCode.OK)
                    {
                        results = response.Results.Chats;
                    }
                }
            }
            else
            {
                var response = await _chatAPIService.GetChats(new Services.APIServices.Params.GetChatsParams() { UserId = GlobalSettings.LoggedUser.UserId }, cancellationToken);
                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    results = response.Results.Chats;
                }
            }
            return results;
        }

        #endregion
    }
}

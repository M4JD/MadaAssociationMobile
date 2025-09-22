using ConnectCareMobile.Common.Global;
using ConnectCareMobile.Interfaces;
using ConnectCareMobile.Models.Base;
using ConnectCareMobile.Services.APIServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectCareMobile.Models
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

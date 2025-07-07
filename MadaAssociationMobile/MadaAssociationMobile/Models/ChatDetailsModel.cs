using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Models.Base;
using MadaAssociationMobile.Services.APIServices.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Models
{
    public class ChatDetailsModel : BaseModel
    {

        private IMessagesAPIService _messagesAPIService;
        public ChatDetailsModel(IMessagesAPIService messagesAPIService)
        {
            _messagesAPIService = messagesAPIService;
        }
        #region Functions
        public async Task<List<MessagesResponse>> GetMessages(Guid ChatId, Guid UserId, CancellationToken cancellationToken)
        {
            var results = new List<MessagesResponse>();
            var response = await _messagesAPIService.GetMessages(new Services.APIServices.Params.GetMessagesParams()
            {
                ChatId = ChatId,
                UserId = UserId
            }, cancellationToken);
            if (response != null && response.Status == System.Net.HttpStatusCode.OK)
            {
                return response.Results?.Messages;
            }
            return results;
        }
        public async Task<bool> SendMessage(Guid ChatId, Guid UserId, string Content, string MessageType, CancellationToken cancellationToken)
        {
            var results = new List<MessagesResponse>();
            var response = await _messagesAPIService.SendMessage(new Services.APIServices.Params.SendMessageParams()
            {
                ChatId = ChatId,
                Content = Content,
                MessageType = MessageType,
                UserId = UserId
            }, cancellationToken);
            if (response != null && response.Status == System.Net.HttpStatusCode.OK)
            {

            }
            return true;
        }
        #endregion
    }
}

using System;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class CreateConnectionParams : BaseParams
    {
        public Guid UserId { get; set; }
        public string FriendUserName { get; set; }

    }
}

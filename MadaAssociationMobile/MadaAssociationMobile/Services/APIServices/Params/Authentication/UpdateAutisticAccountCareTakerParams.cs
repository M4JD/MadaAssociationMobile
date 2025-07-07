using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Services.APIServices.Params
{
    public class UpdateAutisticAccountCareTakerParams:BaseParams
    {
        public string Username { get; set; }
        public string CareTakerUsername { get; set; } = "";
    }
}

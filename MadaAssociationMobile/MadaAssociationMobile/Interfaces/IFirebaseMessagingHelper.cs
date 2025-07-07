using MadaAssociationMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Interfaces
{
    public interface IFirebaseMessagingHelper
    {
        string GetFCMToken();
    }
}

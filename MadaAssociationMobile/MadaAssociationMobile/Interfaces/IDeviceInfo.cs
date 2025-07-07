using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MadaAssociationMobile.Interfaces
{
    public interface IDeviceInfo
    {
        string GetDeviceID();
        void SetStatusBarColor(Color color, bool darkStatusBasTint);
    }
}

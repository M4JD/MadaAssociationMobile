using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConnectCareMobile.Interfaces
{
    public interface IDeviceInfo
    {
        string GetDeviceID();
        void SetStatusBarColor(Color color, bool darkStatusBasTint);
    }
}

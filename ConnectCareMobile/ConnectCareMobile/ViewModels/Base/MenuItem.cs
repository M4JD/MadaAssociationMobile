using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.ViewModels.Base
{
    public class MenuItem
    {
        public string Text { get; set; }
        public CommandAsync Command { get; set; }
    }
}

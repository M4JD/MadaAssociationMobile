using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Common.Exceptions
{
    public class NoConnectionException : Exception
    {
        //Overriding the Message property
        public override string Message
        {
            get
            {
                return "You do not have internet connection";
            }
        }
    }
}

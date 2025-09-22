using ConnectCareMobile.Controls;
using ConnectCareMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Global
{
    public class Country : Selectable
    {
        public string Code { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string DefaultPhoneNumber { get; set; }
    }
}

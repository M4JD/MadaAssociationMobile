using MadaAssociationMobile.Controls;
using MadaAssociationMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Global
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

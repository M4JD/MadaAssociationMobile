using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Interfaces
{
    public interface ISelectable
    {
        string Value { get; set; }
        string ImageName { get; set; }
        bool IsSelected { get; set; }
        bool Enabled { get; set; }
        string DisplayValue { get; set; }
        string FullDescription { get; set; }
        string DisplayValueLinkURL { get; set; }
        string DisplayValueLinkName { get; set; }
    }
}

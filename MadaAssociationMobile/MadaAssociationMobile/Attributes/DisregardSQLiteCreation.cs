using System;
namespace MadaAssociationMobile.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DisregardSQLiteCreation : Attribute
    {
    }
}

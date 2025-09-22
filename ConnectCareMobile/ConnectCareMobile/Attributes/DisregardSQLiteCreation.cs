using System;
namespace ConnectCareMobile.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DisregardSQLiteCreation : Attribute
    {
    }
}

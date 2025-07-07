namespace MadaAssociationMobile.Common.Global
{
    public interface ISettings
    {
        string GetSecretKey();
        bool IsConnected();
        string GetDID();
        string APIURL { get; set; }
        string APIToken { get; set; }
        string GetOcpApimSubsrictionKey();
    }
}

using ConnectCareMobile.Common.Global;
using Xamarin.Essentials;

namespace ConnectCareMobile.Helpers
{
    public class Settings : ISettings
    {
        public string APIURL { get; set; }
        public string APIToken { get; set; }
        public string GetSecretKey()
        {
            return "";
        }
        public bool IsConnected()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                return true;
            else return false;
        }
        public string GetDID()
        {
            return "";
        }
        public string GetOcpApimSubsrictionKey()
        {
            return "";
        }
    }
}

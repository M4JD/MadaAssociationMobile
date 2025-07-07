using MadaAssociationMobile.Common.Global.Enum;
using System;
using Xamarin.Essentials;

namespace MadaAssociationMobile.Common.Global
{
    public class AppSharedPrefences
    {
        public static readonly string IDDID = "MobileID";
        public static readonly string IDUDID = "UniqueDeviceId";
        private static readonly string IDLoggedInUser = "LoggedInUser";
        private static readonly string IDLoggedEmailAddress = "LoggedEmailAddress";
        private static readonly string IDLastLoggedEmailAddress = "LastLoggedEmailAddress";
        private static readonly string IDSelectedLanguage = "SelectedLanguage";
        private static readonly string IDIsAutistic = "IsAutistic";
        private static readonly string IDScreenWidth = "ScreenWidth";
        private static readonly string IDIsLanguageSelected = "LanguageSelected";
        private static readonly string IDUserId = "UserId";
        private static readonly string IDIsLoggedIn = "IsLoggedIn";
        private static readonly string IDBiometricsUsed = "BiometricsUsed";
        private static readonly string IDCardsImagesLoadingInProgress = "CardsImagesLoadingInProgress";
        private static readonly string IDProductsLoadingInProgress = "ProductsLoadingInProgress";
        private static readonly string IDProductsLoadingProgressStartTime = "ProductsLoadingProgressStartTime";
        private static readonly string IDCardsImagesLoadingProgressStartTime = "CardsImagesLoadingProgressStartTime";
        private static readonly string IDPoliciesLoadingInProgress = "PoliciesLoadingInProgress";
        private static readonly string IDPublicTokenLoadingInProgress = "PublicTokenLoadingInProgress";
        private static readonly string IDPoliciesLoadingProgressStartTime = "PoliciesLoadingProgressStartTime";
        private static readonly string IDPublicTokenLoadingProgressStartTime = "PublicTokenLoadingProgressStartTime";
        private static readonly string IDPolicyPDFLoadingProgressStartTime = "PolicyPDFLoadingProgressStartTime";
        private static readonly string IDSqliteTableCreationInProcess = "SqliteTableCreationInProcess";
        private static readonly string IDTranslationsLoaded = "IDTranslationsLoaded";
        private static readonly string IDAPIPrivateToken = "APIPrivateToken";
        private static readonly string IDAPIPublicToken = "APIPublicToken";
        private static readonly string IDProxyURL = "IDPROXYURL";
        public static readonly string IDIsBiometricsChecked = "IDIsBiometricsChecked";

        public static readonly string IDTACDocument = "";
        public static readonly string IDTACAcknowledgeVersion = "IDTACAcknowledgeVersion";
        public static readonly string IDFCMTokenChanged = "IDFCMTokenChanged";
        public static readonly string IDFCMToken = "IDFCMToken";
        public static readonly string IDRedirectionURL = "IDRedirectionURL";
        public static readonly string IDNotificationRedirectionURL = "IDNotificationRedirectionURL";

        public static readonly int CardImageDownloadingRetries = 3;
        public static readonly int DocumentDownloadingRetries = 3;
        public static readonly int APICallsRetries = 3;
        public static readonly double RetryDelayInSeconds = 0.5;
        public static readonly int PublicTokenCallRetries = 5;

        public static readonly int APICallsTimeOut = 30;

        private static AppSharedPrefences _Instance;
        public static AppSharedPrefences Instance
        {
            get
            {
                if (_Instance == null) _Instance = new AppSharedPrefences();
                return _Instance;
            }
        }

        public static void ResetLogoutSharedPreferences()
        {
            Instance.PublicTokenLoadingInProgress = false;
            Instance.APIPrivateToken = string.Empty;
            Instance.APIPublicToken = string.Empty;
            Instance.IsLoggedIn = false;
            Instance.LoggedInEmailAddress = string.Empty;
        }
        public static void StartUpResetSharedPreferences()
        {
            Instance.ScreenWidth = -1;
            Instance.CardsImagesLoadingInProgress = false;
            Instance.SqliteTableCreationInProcess = true;
            Instance.PoliciesLoadingInProgress = false;
            Instance.ProductsLoadingInProgress = false;
            Instance.RedirectionURL = string.Empty;
            Instance.NotificationRedirectionURL = string.Empty;
            ResetLogoutSharedPreferences();
        }

        public int SelectedLanguage
        {
            get
            { return Preferences.Get(IDSelectedLanguage, (int)LanguageEnum.En); }
            set
            { Preferences.Set(IDSelectedLanguage, value); }
        }
        public bool IsAutistic
        {
            get
            { return Preferences.Get(IDIsAutistic, false); }
            set
            { Preferences.Set(IDIsAutistic, value); }
        }
        public double ScreenWidth
        {
            get
            { return Preferences.Get(IDScreenWidth, (double)-1); }
            set
            { Preferences.Set(IDScreenWidth, value); }
        }
        public bool BiometricsUsed
        {
            get
            { return Preferences.Get(IDBiometricsUsed, false); }
            set
            { Preferences.Set(IDBiometricsUsed, value); }
        }
        public bool TranslationsLoaded
        {
            get
            { return Preferences.Get(IDTranslationsLoaded, false); }
            set
            { Preferences.Set(IDTranslationsLoaded, value); }
        }
        public bool ProductsLoadingInProgress
        {
            get
            {
                var cardsLoading = Preferences.Get(IDProductsLoadingInProgress, false);
                if (!cardsLoading)
                    return false;
                //THIS IS A CHECKING TO RELEASE 'ProductsLoadingInProgress' IN CASE IS STUCK TO TRUE
                var productsLoadingProgressStarTime = Convert.ToDateTime(ProductsLoadingProgressStarTime);
                TimeSpan timeSpan = DateTime.Now - productsLoadingProgressStarTime;
                if (timeSpan.TotalSeconds > APICallsRetries * (RetryDelayInSeconds + APICallsTimeOut))
                {
                    Preferences.Set(IDProductsLoadingInProgress, false);
                    return false;
                }
                else
                    return true;
            }
            set
            {
                ProductsLoadingProgressStarTime = DateTime.Now.ToString();
                Preferences.Set(IDProductsLoadingInProgress, value);
            }
        }
        public bool CardsImagesLoadingInProgress
        {
            get
            {
                var cardsLoading = Preferences.Get(IDCardsImagesLoadingInProgress, false);
                if (!cardsLoading)
                    return false;
                //THIS IS A CHECKING TO RELEASE 'CardsImagesLoadingInProgress' IN CASE IS STUCK TO TRUE
                var cardsImagesLoadingProgressStarTime = Convert.ToDateTime(CardsImagesLoadingProgressStarTime);
                TimeSpan timeSpan = DateTime.Now - cardsImagesLoadingProgressStarTime;
                if (timeSpan.TotalSeconds > CardImageDownloadingRetries * (RetryDelayInSeconds + APICallsTimeOut))
                {
                    Preferences.Set(IDCardsImagesLoadingInProgress, false);
                    return false;
                }
                else
                    return true;
            }
            set
            {
                CardsImagesLoadingProgressStarTime = DateTime.Now.ToString();
                Preferences.Set(IDCardsImagesLoadingInProgress, value);
            }
        }
        public bool PoliciesLoadingInProgress
        {
            get
            {
                var policiesLoading = Preferences.Get(IDPoliciesLoadingInProgress, false);
                if (!policiesLoading)
                    return false;
                //THIS IS A CHECKING TO RELEASE 'PoliciesLoadingProgressStarTime' IN CASE IS STUCK TO TRUE
                var policiesLoadingProgressStarTime = Convert.ToDateTime(PoliciesLoadingProgressStarTime);
                TimeSpan timeSpan = DateTime.Now - policiesLoadingProgressStarTime;
                if (timeSpan.TotalSeconds > APICallsRetries * (RetryDelayInSeconds + APICallsTimeOut))
                {
                    Preferences.Set(IDPoliciesLoadingInProgress, false);
                    return false;
                }
                else
                    return true;
            }
            set
            {
                PoliciesLoadingProgressStarTime = DateTime.Now.ToString();
                Preferences.Set(IDPoliciesLoadingInProgress, value);
            }
        }

        public bool PublicTokenLoadingInProgress
        {
            get
            {
                var publicTokenLoading = Preferences.Get(IDPublicTokenLoadingInProgress, false);
                if (!publicTokenLoading)
                    return false;
                //THIS IS A CHECKING TO RELEASE 'PublicTokenLoadingProgressStarTime' IN CASE IS STUCK TO TRUE
                var publicTokenLoadingProgressStarTime = Convert.ToDateTime(PublicTokenLoadingProgressStarTime);
                TimeSpan timeSpan = DateTime.Now - publicTokenLoadingProgressStarTime;
                if (timeSpan.TotalSeconds > PublicTokenCallRetries * (RetryDelayInSeconds + APICallsTimeOut))
                {
                    Preferences.Set(IDPublicTokenLoadingInProgress, false);
                    return false;
                }
                else
                    return true;
            }
            set
            {
                PublicTokenLoadingProgressStarTime = DateTime.Now.ToString();
                Preferences.Set(IDPublicTokenLoadingInProgress, value);
            }
        }
        public string ProductsLoadingProgressStarTime
        {
            get
            { return Preferences.Get(IDProductsLoadingProgressStartTime, DateTime.Now.ToString()); }
            set
            { Preferences.Set(IDProductsLoadingProgressStartTime, value); }
        }
        public string CardsImagesLoadingProgressStarTime
        {
            get
            { return Preferences.Get(IDCardsImagesLoadingProgressStartTime, DateTime.Now.ToString()); }
            set
            { Preferences.Set(IDCardsImagesLoadingProgressStartTime, value); }
        }
        public string PoliciesLoadingProgressStarTime
        {
            get
            { return Preferences.Get(IDPoliciesLoadingProgressStartTime, DateTime.Now.ToString()); }
            set
            { Preferences.Set(IDPoliciesLoadingProgressStartTime, value); }
        }
        public string PublicTokenLoadingProgressStarTime
        {
            get
            { return Preferences.Get(IDPublicTokenLoadingProgressStartTime, DateTime.Now.ToString()); }
            set
            { Preferences.Set(IDPublicTokenLoadingProgressStartTime, value); }
        }
        public string PolicyPDFLoadingProgressStarTime
        {
            get
            { return Preferences.Get(IDPolicyPDFLoadingProgressStartTime, DateTime.Now.ToString()); }
            set
            { Preferences.Set(IDPolicyPDFLoadingProgressStartTime, value); }
        }
        public bool SqliteTableCreationInProcess
        {
            get
            { return Preferences.Get(IDSqliteTableCreationInProcess, true); }
            set
            { Preferences.Set(IDSqliteTableCreationInProcess, value); }
        }
        public bool IsLanguageSelected
        {
            get
            { return Preferences.Get(IDIsLanguageSelected, false); }
            set
            { Preferences.Set(IDIsLanguageSelected, value); }
        }
        public bool IsLoggedIn
        {
            get
            { return Preferences.Get(IDIsLoggedIn, false); }
            set
            {
                if (value == false)
                    LoggedInUser = string.Empty;
                Preferences.Set(IDIsLoggedIn, value);
            }
        }
        public string UserId
        {
            get
            { return Preferences.Get(IDUserId, string.Empty); }
            set
            {
                Preferences.Set(IDUserId, value);
            }
        }
        public string UDID
        {
            get
            { return Preferences.Get(IDUDID, null); }
            set
            {
                Preferences.Set(IDUDID, value);
            }
        }
        public string DID
        {
            get
            { return Preferences.Get(IDDID, null); }
            set
            {
                Preferences.Set(IDDID, value);
            }
        }

        public string APIPrivateToken
        {
            get
            { return Preferences.Get(IDAPIPrivateToken, string.Empty); }
            set
            { Preferences.Set(IDAPIPrivateToken, value); }
        }
        public string APIPublicToken
        {
            get
            { return Preferences.Get(IDAPIPublicToken, string.Empty); }
            set
            { Preferences.Set(IDAPIPublicToken, value); }
        }
        public string LoggedInUser
        {
            get
            { return Preferences.Get(IDLoggedInUser, string.Empty); }
            set
            { Preferences.Set(IDLoggedInUser, value); }
        }
        public string LastLoggedInEmailAddress
        {
            get
            { return Preferences.Get(IDLastLoggedEmailAddress, string.Empty); }
            set
            { Preferences.Set(IDLastLoggedEmailAddress, value); }
        }
        public string LoggedInEmailAddress
        {
            get
            { return Preferences.Get(IDLoggedEmailAddress, string.Empty); }
            set
            { Preferences.Set(IDLoggedEmailAddress, value); }
        }
        public string ProxyURL
        {
            get
            { return Preferences.Get(IDProxyURL, ""); }
            set
            { Preferences.Set(IDProxyURL, value); }
        }
        public string TACAcknowledgeVersion
        {
            get
            { return Preferences.Get(IDTACAcknowledgeVersion, ""); }
            set
            { Preferences.Set(IDTACAcknowledgeVersion, value); }
        }
        public string TACDocument
        {
            get
            { return Preferences.Get(IDTACDocument, ""); }
            set
            { Preferences.Set(IDTACDocument, value); }
        }
        public bool FCMTokenChanged
        {
            get
            { return Preferences.Get(IDFCMTokenChanged, false); }
            set
            { Preferences.Set(IDFCMTokenChanged, value); }
        }
        public string FCMToken
        {
            get
            { return Preferences.Get(IDFCMToken, string.Empty); }
            set
            { Preferences.Set(IDFCMToken, value); }
        }
        public string RedirectionURL
        {
            get
            { return Preferences.Get(IDRedirectionURL, string.Empty); }
            set
            { Preferences.Set(IDRedirectionURL, value); }
        }
        public string NotificationRedirectionURL
        {
            get
            { return Preferences.Get(IDNotificationRedirectionURL, string.Empty); }
            set
            { Preferences.Set(IDNotificationRedirectionURL, value); }
        }
        public bool IsBiometricsChecked
        {
            get
            { return Preferences.Get(IDIsBiometricsChecked, false); }
            set
            { Preferences.Set(IDIsBiometricsChecked, value); }
        }
    }
}

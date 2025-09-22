using System.ComponentModel;

namespace ConnectCareMobile.Common.Global.Enum
{
    public enum ClaimStatus
    {
        Pending = 1,
        Voided = 2,
        UnderReview = 3,
        Approved = 4
    }
    public enum LanguageEnum
    {
        Ar = 1,
        En = 2
    }
    public enum MessagingCenterTokenEnum
    {
        Language = 1,
        ApplinksToLogin = 2,
        ApplinksToResetPassword = 3,
        SignalRQuoteSummary = 4,
        SignalRQuoteCheckBlackListed = 5,
        SignalRPoliciesUpdated = 6,
        NotificationCountUpdate = 7,
        SignalRActionCenterData = 8,
        TooltipAction = 9
    }
    public enum PopoupResultEnum
    {
        None = 0,
        Proceed = 1,
        Discard = 2
    }
    public enum EasingEnum
    {
        Linear = 1,
        SinOut = 2,
        SinIn = 3,
        SinInOut = 4,
        CubicIn = 5,
        CubicOut = 6,
        CubInOut = 7,
        BounceOut = 8,
        BounceIn = 9,
        SpringIn = 10,
        SpringOut = 11
    }
    public enum DirectionEnum
    {
        Left = 1,
        Up = 2,
        Right = 3,
        Down = 4
    }
    public enum APIResponseStatuses
    {
        [Description("Failed")]
        Failed = 0,
        [Description("Success")]
        Success = 1,
        [Description("Rejected")]
        Rejected = 2,
        [Description("Pending")]
        Pending = 3,
        [Description("Under Processing")]
        UnderProcessing = 4,
        [Description("Requires MFA")]
        RequireMFA = 5,
        [Description("Error")]
        Error = 6
    }
    public enum UserStatus : byte
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Active")]
        Active = 1,
        [Description("Black Listed")]
        BlackListed = 2,
        [Description("Locked")]
        Locked = 3,
        [Description("Pending Confirmation")]
        PendingConfirmation = 4,
        [Description("Needs Mobile Verification")]
        NeedsMobileVerification = 5,
        [Description("Prospect")]
        Prospect = 6,
        [Description("Allowed")]
        Allowed = 7
    }
    public enum APIHandlingResult
    {
        Success = 0,
        Failed = 1,
        Handled = 2
    }
    public enum LinerListCellTapActionEnum
    {
        None = 0,
        Navigation = 1,
        FunctionExecution = 2
    }
    public enum AppModeEnum
    {
        Offline = 1,
        Online = 2
    }
    //public enum PolicyTypeEnum
    //{
    //    [Description("-100")]
    //    All = -100,
    //    [Description("1")]
    //    Medical = 1,
    //    [Description("2")]
    //    Motor = 2,
    //    [Description("3")]
    //    Travel = 3,
    //    [Description("Home")]
    //    Home = 4
    //}
    public enum DisplayFeedbackEnum
    {
        None = 0,
        Positive = 1,
        Negative = 2,
        Both
    }
    public enum DownloadingStateEnum
    {
        None = 0,
        ReadyToStart = 1,
        InProgress = 2,
        Success = 3,
        Failed = 4
    }
    public enum PublicTokenStatusEnum
    {
        Empty = 0,
        AlreadyExits = 1,
        IsFetched = 2,
        Initial = 3
    }
    public enum QuotationAnswerTypeCodeEnum
    {
        Text = 1,
        DropDown = 2,
        DatePicker = 3,
        RadioButton = 4,
        MultipleSelect = 5,
        FileUpload = 6,
        Spinner = 7,
        NumberText = 8,
        TextArea = 9,
        DateTimeRange = 10,
        Country = 11,
        Nationality = 12,
        GuidHidden = 13,
        Chips = 14,
        OnePictureUpload = 15,
        DateTimePicker = 16,
        YearDatePicker = 17,
        RadioButtonWithDescription = 18,
        RadioButtonWithAdditionalInfo = 19,
        DatePickerWithDateRangeSelection = 20,
        FormatedNumberText = 21
    }
    public enum ClaimAnswerTypeCodeEnum
    {
        Text = 1,
        DropDown = 2,
        DatePicker = 3,
        RadioButton = 4,
        MultipleSelect = 5,
        FileUpload = 6,
        Spinner = 7,
        NumberText = 8,
        TextArea = 9,
        DateTimeRange = 10,
        Country = 11,
        Nationality = 12,
        GuidHidden = 13,
        Chips = 14,
        OnePictureUpload = 15,
        DateTimePicker = 16,
        YearDatePicker = 17
    }
    public enum RelationshipEnum
    {
        [Description("Main policyholder")]
        MainPolicyHolder = 0,
        [Description("Wife")]
        Wife = 1,
        [Description("Husband")]
        Husband = 2,
        [Description("Son")]
        Son = 3,
        [Description("Daughter")]
        Daughter = 4
    }
    public enum GenderEnum
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2
    }
    public enum MCActionEnum
    {
        Add = 1,
        Remove = 2
    }
    public enum DocumentUploadingStatusEnum
    {
        None = 0,
        Initial = 1,
        Uploading = 2,
        Uploaded = 3,
        DownloadFailed = 4
    }
    //public enum TermsAndConditionsFileEnum
    //{
    //    Medcial = 1,
    //    Motor = 2,
    //    Travel = 3
    //}
    public enum PinTypeEnum
    {
        BluePin = 1,
        RedPin = 2,
        OrangePin = 3
    }
    public enum BillPaymentWebViewActionsEnum
    {
        Cancel = 1,
        DownloadReceipt = 2,
        CreateAccount = 3,
        AfterPaymentSteps = 4
    }
    public enum PolicySelectionFlowEnum
    {
        Normal = 1,
        ClaimsCreation = 2
    }
    public enum DocumentTypeEnum
    {
        Image = 1,
        Document = 2
    }
    public enum UploadActionEnum
    {
        Success = 1,
        FailedFileValidation = 2,
        Failed = 3,
        FailedMaxFiles = 4,
        FailedFileType =5
    }
    public enum UploadDocumentModeEnum
    {
        Edit = 1,
        ViewOnly = 2
    }
    public enum ProviderDetailPopupActionEnum
    {
        Call = 1,
        Direction = 2
    }
    public enum ActionCenterStatuses
    {
        Informational = 1,
        Warning = 2,
        Critical = 3
    }
    public enum BiometricStateEnum
    {
        NotAvailable = 1,
        Missing = 2,
        Saved = 3
    }
    public enum BiometricTypeEnum
    {
        None = 1,
        Fingerprint = 2,
        FaceID = 3
    }
    public enum TooltipPositionEnum
    {
        Top,
        Bottom,
        Left,
        Right
    }
}

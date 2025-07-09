using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.Package
{
  public enum SalePaymentResponse
  {
    #region Parsian

    Parsian_UnkownError = -32768,
    Parsian_PaymentRequestIsNotEligibleToReversal = -1552,
    Parsian_PaymentRequestIsAlreadyReversed = -1551,
    Parsian_PaymentRequestStatusIsNotReversalable = -1550,
    Parsian_MaxAllowedTimeToReversalHasExceeded = -1549,
    Parsian_BillPaymentRequestServiceFailed = -1548,
    Parsian_InvalidConfirmRequestService = -1540,
    Parsian_TopupChargeServiceTopupChargeRequestFailed = -1536,
    Parsian_PaymentIsAlreadyConfirmed = -1533,
    Parsian_MerchantHasConfirmedPaymentRequest = -1532,
    Parsian_CannotConfirmNonSuccessfulPayment = -1531,
    Parsian_MerchantConfirmPaymentRequestAccessVaiolated = -1530,
    Parsian_ConfirmPaymentRequestInfoNotFound = -1528,
    Parsian_CallSalePaymentRequestServiceFailed = -1527,
    Parsian_ReversalCompleted = -1507,
    Parsian_PaymentConfirmRequested = -1505,
    Parsian_InvalidMinimumPaymentAmount = -132,

    [Description("کاربر دکمه بازگشت رو زده")]
    Parsian_PressBackButton = -138,

    Parsian_InvalidToken = -131,
    Parsian_TokenIsExpired = -130,
    Parsian_InvalidIpAddressFormat = -128,
    Parsian_InvalidMerchantIp = -127,
    Parsian_InvalidMerchantPin = -126,
    Parsian_InvalidStringIsNumeric = -121,
    Parsian_InvalidLength = -120,
    Parsian_InvalidOrganizationId = -119,
    Parsian_ValueIsNotNumeric = -118,
    Parsian_LenghtIsLessOfMinimum = -117,
    Parsian_LenghtIsMoreOfMaximum = -116,
    Parsian_InvalidPayId = -115,
    Parsian_InvalidBillId = -114,
    Parsian_ValueIsNull = -113,
    Parsian_OrderIdDuplicated = -112,
    Parsian_InvalidMerchantMaxTransAmount = -111,
    Parsian_ReverseIsNotEnabled = -108,
    Parsian_AdviceIsNotEnabled = -107,
    Parsian_ChargeIsNotEnabled = -106,
    Parsian_TopupIsNotEnabled = -105,
    Parsian_BillIsNotEnabled = -104,
    Parsian_SaleIsNotEnabled = -103,
    Parsian_ReverseSuccessful = -102,
    Parsian_MerchantAuthenticationFailed = -101,
    Parsian_MerchantIsNotActive = -100,
    Parsian_Server_Error = -1,

    [Description("عملیات موفق می باشد")]
    Parsian_Successful = 0,

    Parsian_Refer_To_Card_Issuer_Decline = 1,
    Parsian_Refer_To_Card_Issuer_Special_Conditions = 2,
    Parsian_Invalid_Merchant = 3,
    Parsian_Do_Not_Honour = 5,
    Parsian_Error = 6,
    Parsian_Honour_With_Identification = 8,
    Parsian_Request_Inprogress = 9,
    Parsian_Approved_For_Partial_Amount = 10,
    Parsian_Invalid_Transaction = 12,
    Parsian_Invalid_Amount = 13,
    Parsian_Invalid_Card_Number = 14,
    Parsian_No_Such_Issuer = 15,
    Parsian_Customer_Cancellation = 17,
    Parsian_Invalid_Response = 20,
    Parsian_No_Action_Taken = 21,
    Parsian_Suspected_Malfunction = 22,
    Parsian_Format_Error = 30,
    Parsian_Bank_Not_Supported_By_Switch = 31,
    Parsian_Completed_Partially = 32,
    Parsian_Expired_Card_Pick_Up = 33,
    Parsian_Allowable_PIN_Tries_Exceeded_Pick_Up = 38,
    Parsian_No_Credit_Acount = 39,
    Parsian_Requested_Function_is_not_supported = 40,
    Parsian_Lost_Card = 41,
    Parsian_Stolen_Card = 43,
    Parsian_Bill_Can_not_Be_Payed = 45,
    Parsian_No_Sufficient_Funds = 51,
    Parsian_Expired_Account = 54,
    Parsian_Incorrect_PIN = 55,
    Parsian_No_Card_Record = 56,
    Parsian_Transaction_Not_Permitted_To_CardHolder = 57,
    Parsian_Transaction_Not_Permitted_To_Terminal = 58,
    Parsian_Suspected_Fraud_Decline = 59,
    Parsian_Exceeds_Withdrawal_Amount_Limit = 61,
    Parsian_Restricted_Card_Decline = 62,
    Parsian_Security_Violation = 63,
    Parsian_Exceeds_Withdrawal_Frequency_Limit = 65,
    Parsian_Response_Received_Too_Late = 68,
    Parsian_Allowabe_Number_Of_PIN_Tries_Exceeded = 69,
    Parsian_PIN_Reties_Exceeds_Slm = 75,
    Parsian_Deactivated_Card_Slm = 78,
    Parsian_Invalid_Amount_Slm = 79,
    Parsian_Transaction_Denied_Slm = 80,
    Parsian_Cancelled_Card_Slm = 81,
    Parsian_Host_Refuse_Slm = 83,
    Parsian_Issuer_Down_Slm = 84,
    Parsian_Issuer_Or_Switch_Is_Inoperative = 91,
    Parsian_Financial_Inst_Or_Intermediate_Net_Facility_Not_Found_for_Routing = 92,
    Parsian_Tranaction_Cannot_Be_Completed = 93

    #endregion
  }
  public class ClientSalePaymentResponseData
  {
    public long Token { get; set; }
    public short Status { get; set; }
    public int OrderId { get; set; }
    public int TerminalNo { get; set; }
    public int RRN { get; set; }
  }

  public class PaymentViewModel
  {
    public SalePaymentResponse SalePaymentResponse { get; set; }
    public long Token { get; set; }
    public string Message { get; set; }
    public string TrackCode { get; set; }
    public string ReserveCode { get; set; }
    public bool Status { get; set; }
  }
}

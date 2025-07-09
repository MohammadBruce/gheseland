using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using gheseland.Services;
using gheseland.Services.Implements;
using gheseland.ViewModel.Package;

namespace gheseland.Controllers
{
    public partial class PaymentController : Controller
    {
        private readonly IHttpService _httpservice;
        private readonly UserService _userService;
        private static string _privateKey;
        private const string baseUrl = "http://api.gheseland.com";
        private readonly string packagesUrl = baseUrl + "/api/Pakages/GetPakagesList/";
        private readonly string prefactorUrl = baseUrl + "/api/Payment/PreFactor/";
        private readonly string makeRequestUrl = baseUrl + "/api/Payment/MakeRequest";

        public PaymentController()
        {
            _httpservice = new HttpService();
            _userService = new UserService();
            _privateKey = "<RSAKeyValue><Modulus>vFSYV9Smz3nmXSfAprWIcHrMl8BHSCaBmitRrv9CZQprHzjCcVCr25wdn2PLCnPNwIrOVPUYXL7M7eRenTIcBnSbs7/7gWsADKsov0bY0box5J+6keBHYlbmc2rL6eUzxRRlI4Ecr6VW9DGE5c02yDwGOBVaYgnjoB8mypaOKZM=</Modulus><Exponent>AQAB</Exponent><P>9l7WUuQVFgiseC7sQ7B95rYo9IM03AAxwzP2r0mVPFNOssKhvzqzXEVRU3qsNPCAjaqI/x+kKn6qnw16f0TjpQ==</P><Q>w7EDp7P/kYPR552ZMAVeAX69fFEEDMdblQ9UZ2qdX5cfjVGkXRCQkMUV7RmWePQ3jhHEgeQy6ve18n+lZq3y1w==</Q><DP>t0KSMDRijvJCwF8+9ZbZ//xt+HuEnUUzvtzvWTHl5WqedpAaWrGiazdQqETuxa5EIZzBrOxvZAC2j/sprHOk1Q==</DP><DQ>bTrQJHq3S5z9vD/essw8Ja99yvdZwFxCxVgSL8t13lZ3WSVaDkxhtT0dOQQQVqFSpS1Lt4kN1tO/roULaN3tbw==</DQ><InverseQ>V7rofOBbbvp2Mr7/rgsRGMi0E8waQ6flDFCv78ANPU0rwQYnvk2kSNZMwKACd4ithp4W4MpSFaw1KCGo7W21hQ==</InverseQ><D>c1e0Pgala2iTJ/aSzQddWtY6u64tegjrU0q4ql73gP6SgPq4S8JGnyfMFLR/xBUMrKHWoL3Df/nzdLdrIwdvlY3S3TtBG3nRKhMJlviy7q7/LYX32gErL34M20Q1ijG+UwmRxSZcBIdJiwB9wMoL7oh3/M2A+3Wb+1J0DNBBMPE=</D></RSAKeyValue>";
        }
        // GET: Payment
        public virtual async Task<ActionResult> Index()
        {
            var user = _userService.GetUserInfo();
            if (user == null)
            {
                return RedirectToAction(MVC.Auth.RegisterUser());
            }
            var result = await _httpservice.GetAsync<IEnumerable<PackageViewModel>>(null, packagesUrl);

            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Detail(int? packageId, string giftCode)
        {
            var user = _userService.GetUserInfo();
            if (user == null)
            {
                return RedirectToAction(MVC.Auth.RegisterUser());
            }
            bool haveGiftCode = !string.IsNullOrEmpty(giftCode);
            if (!haveGiftCode)
            {
                var parameters = new
                {
                    UserID = user.UserID.ToString(),
                    PakageID = packageId.ToString()
                };
                var prefactor = await _httpservice.PostAsync<PackagePrefactorViewModel>(parameters, prefactorUrl);
                if (!prefactor.Result)
                {
                    return RedirectToAction(MVC.Payment.Index());
                }
                prefactor.PackageId = packageId.Value;
                return View(prefactor);
            }
            else
            {
                var parametersWithGiftCode = new
                {
                    UserID = user.UserID.ToString(),
                    PakageID = packageId.ToString(),
                    GiftCode = giftCode
                };
                var prefactor = await _httpservice.PostAsync<PackagePrefactorViewModel>(parametersWithGiftCode, prefactorUrl);
                if (!prefactor.Result)
                {
                    return RedirectToAction(MVC.Payment.Index());
                }
                prefactor.PackageId = packageId.Value;
                prefactor.GiftCode = giftCode;
                return View(prefactor);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ConnectingToBank(string userId, int packageId, string giftCode)
        {
            var user = _userService.GetUserInfo();
            if (user == null)
            {
                return RedirectToAction(MVC.Auth.RegisterUser());
            }
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            return Redirect($"{baseUrl}/connecttobank.aspx?uid={userId}&pid={packageId}&gCode={giftCode}&RequestType=5");
        }



        #region MoneyTransfer

        public static Dictionary<string, object> MoneyTransfer(string amount, string invoiceNumber, int gateway, string dateTime, string giftCode)
        {


            if (gateway == 1)//بانک پاسارگاد
            {
                string merchantCode = ConfigurationManager.AppSettings["MerchantCode"];
                string terminalCode = ConfigurationManager.AppSettings["TerminalCode"];
                string action = "1003";// 1003=واریز به حساب
                                       // string redirectAddress = "http://localhost:1446/BackFromBank.aspx?transId=" + invoiceNumber + "&gCode=" + giftCode + "&tDate=" + dateTime;
                string redirectAddress = "http://koodaksalam.ir/BackFromBank.aspx?transId=" + invoiceNumber + "&gCode=" + giftCode + "&tDate=" + dateTime;

                string PrivateKey = _privateKey;

                string timeStamp = dateTime;
                string invoiceDate = dateTime;


                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(PrivateKey);

                string data = "#" + merchantCode + "#" + terminalCode + "#" + invoiceNumber +
                              "#" + invoiceDate + "#" + amount + "#" + redirectAddress + "#" + action + "#" + timeStamp +
                              "#";

                byte[] signedData = rsa.SignData(Encoding.UTF8.GetBytes(data), new SHA1CryptoServiceProvider());

                string signedString = Convert.ToBase64String(signedData);


                Dictionary<string, object> datacollection = new Dictionary<string, object>
        {
          {"merchantCode", merchantCode},
          {"terminalCode", terminalCode},
          {"amount", amount},
          {"redirectAddress", redirectAddress},
          {"invoiceNumber", invoiceNumber},
          {"invoiceDate", invoiceDate},
          {"action", action},
          {"sign", signedString},
          {"timeStamp", timeStamp}
      };
                return datacollection;
            }


            if (gateway == 2)//بانک ملت
            {
                ////
                ///

            }
            return null;
        }

        public static string ReadPaymentResult(HttpRequest pageRequest)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pep.shaparak.ir/CheckTransactionResult.aspx");
            string text = "invoiceUID=" + pageRequest.QueryString["tref"];
            byte[] textArray = Encoding.UTF8.GetBytes(text);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = textArray.Length;
            request.GetRequestStream().Write(textArray, 0, textArray.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            return result;
        }
        public static bool ConfirmPrice(string amount, string invoiceNumber, string invoiceDate)
        {
            string merchantCode = ConfigurationManager.AppSettings["MerchantCode"];
            string terminalCode = ConfigurationManager.AppSettings["TerminalCode"];
            AppSettingsReader appRead = new AppSettingsReader();

            string PrivateKey = _privateKey;

            string timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKey);

            string data = "#" + merchantCode + "#" + terminalCode + "#" + invoiceNumber + "#" + invoiceDate + "#" + amount + "#" + timeStamp + "#";

            byte[] signedData = rsa.SignData(Encoding.UTF8.GetBytes(data), new SHA1CryptoServiceProvider());

            string signedString = Convert.ToBase64String(signedData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pep.shaparak.ir/VerifyPayment.aspx");
            string text = "InvoiceNumber=" + invoiceNumber + "&InvoiceDate=" +
                          invoiceDate + "&MerchantCode=" + merchantCode + "&TerminalCode=" +
                          terminalCode + "&Amount=" + amount + "&TimeStamp=" + timeStamp + "&Sign=" + signedString;
            byte[] textArray = Encoding.UTF8.GetBytes(text);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = textArray.Length;
            request.GetRequestStream().Write(textArray, 0, textArray.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(result);
            XmlElement oElResult = (XmlElement)oXml.SelectSingleNode("//result"); //نتیجه تراکنش
            return Convert.ToBoolean(oElResult.InnerText); // xmlResult.DocumentContent = result;
        }

        #endregion
        #region ApiList

        #endregion
    }
}
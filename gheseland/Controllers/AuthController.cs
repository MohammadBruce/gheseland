using gheseland.Common;
using gheseland.Services;
using gheseland.Services.Implements;
using gheseland.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace gheseland.Controllers
{
    public partial class AuthController : Controller
    {
        private readonly IHttpService _httpservice;
        private readonly IUserService _user;
        private const string baseUrl = "http://api.gheseland.com";
        private const string defaultEXT = "+98";
        private readonly string registerWithPhoneStr = baseUrl + "/api/Register/RegisterUserWithPhone";
        private readonly string loginWithPhoneStr = baseUrl + "/api/Login/LoginWithRegCode";
        private readonly string locationList = baseUrl + "/api/Location/GetCountryList";
        public AuthController()
        {
            _httpservice = new HttpService();
            _user = new UserService();
        }


        public virtual ActionResult RegisterUser(string ext)
        {
            if (_user.IsIntroduced())
            {
                ViewBag.IsIntroduced = "t";
            }
            else
            {
                ViewBag.IsIntroduced = "f";
            }
            if (ext == "true")
            {
                _user.RemoveUserId();
            }
            else
            {
                if (_user.GetUserInfo() != null)
                {
                    return RedirectToAction(MVC.Story.Index());
                }
            }

            ViewBag.RegType = "register";
            var contryList = GetContryList();

            return View(contryList);
        }

        private IEnumerable<CountryViewModel> GetContryList()
        {
            return _httpservice.Get<IEnumerable<CountryViewModel>>(null, locationList);
        }
        private string[] m_Patterns = new string[] {
          @"^\+989[0-9]{9}$"
        };

        private string MakeCombinedPattern()
        {
            return string.Join("|", m_Patterns
              .Select(item => "(" + item + ")"));
        }
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                bool isEmail = Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                return isEmail;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult RegisterUser(string ext, string phoneNumber, string email)
        {
            var contryList = GetContryList();
            if (phoneNumber.Length < 10)
            {
                ViewBag.RegType = "register";
                ViewBag.PhoneNumber = phoneNumber;
                ViewBag.Email = email;
                ViewBag.Ext = ext;

                ViewBag.ErrorMessage = "شماره ی وارد شده معتبر نیست";
                return View(MVC.Auth.Views.RegisterUser, contryList);
            }
            var phone = phoneNumber.ToGlobalPhone(ext);
            if (ext == defaultEXT)
            {
                var phoneValidation = Regex.IsMatch(phone, MakeCombinedPattern());
                if (phone.Substring(0, 4) != "+989" || phone.Length != 13)
                {
                    phoneValidation = false;
                }
                if (!phoneValidation)
                {
                    ViewBag.RegType = "register";
                    ViewBag.PhoneNumber = phoneNumber;
                    ViewBag.Email = email;
                    ViewBag.Ext = ext;

                    ViewBag.ErrorMessage = "شماره ی وارد شده معتبر نیست";
                    return View(MVC.Auth.Views.RegisterUser, contryList);
                }
            }

            if (!string.IsNullOrEmpty(email))
            {
                var emailValidation = IsValidEmail(email);
                if (!emailValidation)
                {
                    ViewBag.RegType = "register";
                    ViewBag.PhoneNumber = phoneNumber;
                    ViewBag.Email = email;
                    ViewBag.Ext = ext;
                    ViewBag.ErrorMessage = "آدرس ایمیل وارد شده معتبر نیست";
                    return View(MVC.Auth.Views.RegisterUser, contryList);
                }
            }



            var data = new
            {
                DeviceBrand = "web",
                DeviceID = "website",
                DeviceModel = "web",
                DeviceNotifictionID = "",
                DeviceOsVersion = "1.0.0.0",
                DeviceScreenSize = "1980*125",
                DeviceStatusID = "1",
                DeviceTypeID = "3",
                Mobile = phone,
                Email = email,
                RegIP = HttpContext.Request.UserHostAddress
            };
            var result = _httpservice.Post(data, registerWithPhoneStr);
            dynamic dResult = result;
            if (bool.Parse(dResult.m_Item1.ToString()))
            {
                ViewBag.RegType = "login";
                ViewBag.PhoneNumber = phoneNumber;
                ViewBag.Email = email;
                ViewBag.Ext = ext;

            }
            else
            {
                ViewBag.RegType = "register";
                ViewBag.ErrorMessage = dResult.m_Item2;
                ViewBag.Email = email;
                ViewBag.Ext = ext;


            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LoginUser(string ext, string email, string phoneNumber, string loginCode)
        {
            var contryList = GetContryList();

            var phone = phoneNumber.ToGlobalPhone(ext);

            var data = new
            {
                Mobile = phone,
                Email = email,
                RegCode = loginCode
            };
            var result = _httpservice.Post(data, loginWithPhoneStr);
            dynamic dResult = result;
            if (bool.Parse(dResult.m_Item1.ToString()))
            {
                _user.SetUserLogin(dResult.m_Item2.ToString());
                return RedirectToAction(MVC.Story.Index());
            }
            else
            {
                ViewBag.RegType = "login";
                ViewBag.PhoneNumber = phoneNumber;
                ViewBag.Email = email;
                ViewBag.Ext = ext;
                ViewBag.ErrorMessage = dResult.m_Item2;
                ViewBag.Ext = ext;

                return View(MVC.Auth.Views.RegisterUser, contryList);
            }
        }
    }
}

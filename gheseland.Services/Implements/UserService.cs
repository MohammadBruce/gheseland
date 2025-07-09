using gheseland.ViewModel.User;
using System;
using System.Web;

namespace gheseland.Services.Implements
{
    public class UserService : IUserService
    {
        private const string USER_KEY = "u";
        private const string PWA_KEY = "pwa";
        private readonly IHttpService _httpservice;
        private const string baseUrl = "http://api.gheseland.com";
        private readonly string userExpireDateStr = baseUrl + "/api/users/UserExpireDate";

        public UserService()
        {
            _httpservice = new HttpService();

        }
        public void SetUserLogin(string userId)
        {
            RemoveUserId();
            var UserCookies = new HttpCookie(USER_KEY) { Value = userId, Expires = DateTime.Now.AddDays(365) };
            HttpContext.Current.Response.Cookies.Add(UserCookies);
        }
        public bool IsIntroduced()
        {
            if (HttpContext.Current.Request.Cookies[PWA_KEY] == null) return false;
            var value = HttpContext.Current.Request.Cookies[PWA_KEY].Value;
            return true;

        }
        public string GetUserId()
        {
            if (HttpContext.Current.Request.Cookies[USER_KEY] == null) return null;
            var value = HttpContext.Current.Request.Cookies[USER_KEY].Value;
            return value;

        }
        public void RemoveUserId()
        {
            if (HttpContext.Current.Request.Cookies[USER_KEY] != null)
            {
                HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies[USER_KEY];
                HttpContext.Current.Response.Cookies.Remove(USER_KEY);
                currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                currentUserCookie.Value = null;
                HttpContext.Current.Response.SetCookie(currentUserCookie);

            }

        }
        public UserInfoViewModel GetUserInfo()
        {
            var userId = GetUserId();
            if (userId != null)
            {
                var param = new
                {
                    UserID = userId

                };

                var result = _httpservice.Post(param, userExpireDateStr);
                dynamic dResult = result;
                var fromDate = dResult.m_Item2.ToString().Split('-')[0];
                var toDate = dResult.m_Item2.ToString().Split('-')[1];
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                return new UserInfoViewModel()
                {
                    UserID = Guid.Parse(userId),
                    ExpireDate = start.AddSeconds(double.Parse(fromDate))
                };
            }

            return null;
        }
    }
}

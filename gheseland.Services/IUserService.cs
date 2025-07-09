using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gheseland.ViewModel.User;

namespace gheseland.Services
{
    public interface IUserService
    {
        void SetUserLogin(string userId);
        string GetUserId();
        bool IsIntroduced();
        UserInfoViewModel GetUserInfo();
        void RemoveUserId();
    }
}

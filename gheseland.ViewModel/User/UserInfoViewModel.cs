using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.ViewModel.User
{
  public class UserInfoViewModel
  {
    public Guid UserID { get; set; }
    public DateTime ExpireDate { get; set; }
    public int ExpireRemainigDays => (ExpireDate - DateTime.Now).Days;
    public int ExpireRemainigHours => (ExpireDate - DateTime.Now).Hours;
    public bool HaveCashValue => (ExpireDate - DateTime.Now).TotalHours>1;
    public string ExpireRemainigTotal => ExpireRemainigDays + " روز و "+ ExpireRemainigHours + " ساعت باقی مانده است ";
  }
}

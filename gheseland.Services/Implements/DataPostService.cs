using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace gheseland.Services.Implements
{
  public class DataPostServices
  {
    private NameValueCollection Inputs = new NameValueCollection();
    private string _mUrl = "";
    private string _mMethod = "post"; //or Get
    private string _mFormName = "form1";

    private string frm = string.Empty;
    public void Post()
    {
      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.Write("<html><head>");
      HttpContext.Current.Response.Write(string.Format("</head><body onload='document.{0}.submit()'>", _mFormName));
      HttpContext.Current.Response.Write(string.Format("<form name={0} method={1} action={2} >", _mFormName, _mMethod, Url));
      for (int i = 0; i < Inputs.Keys.Count; i++)
      {
        HttpContext.Current.Response.Write(string.Format("<input name={0} type='hidden' value={1}>", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
      }
      HttpContext.Current.Response.Write("</form>");
      HttpContext.Current.Response.Write("</body></html>");
      HttpContext.Current.Response.End();
    }

    public void AddKey(string name, string value)
    {
      Inputs.Add(name, value);
    }

    public string Method { get; set; }
    public string FormName { get; set; }
    public string Url { get; set; }

  }
}

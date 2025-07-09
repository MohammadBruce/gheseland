using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gheseland.Services
{
  public interface IHttpService
  {
    /// <summary>
    /// دریافت اطلاعات با استفاده از http با متد GET
    /// </summary>
    /// <param name="objectParams"></param>
    /// <param name="url"></param>
    /// <param name="contenttype"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    object Get(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null);


    /// <summary>
    /// دریافت اطلاعات با استفاده از http با متد GET
    /// </summary>
    /// <param name="objectParams"></param>
    /// <param name="url"></param>
    /// <param name="contenttype"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    T Get<T>(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null);
    /// <summary>
    /// دریافت اطلاعات با استفاده از http با متد POST
    /// </summary>
    /// <param name="objectParams"></param>
    /// <param name="url"></param>
    /// <param name="contenttype"></param>
    /// <param name="headers"></param>
    /// <param name="authorizationKey"></param>
    /// <returns></returns>
    object Post(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null, string authorizationKey = null);

    /// <summary>
    /// دریافت اطلاعات با استفاده از http با متد POST
    /// </summary>
    /// <param name="objectParams"></param>
    /// <param name="url"></param>
    /// <param name="contenttype"></param>
    /// <param name="headers"></param>
    /// <param name="authorizationKey"></param>
    /// <returns></returns>
    T Post<T>(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null, string authorizationKey = null);

    /// <summary>
    ///  دریافت اطلاعات با استفاده از http با متد POST
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectParams"></param>
    /// <param name="url"></param>
    /// <param name="contenttype"></param>
    /// <param name="headers"></param>
    /// <param name="authorizationKey"></param>
    /// <returns></returns>
    Task<T> PostAsync<T>(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null,
      string authorizationKey = null);

    /// <summary>
    ///  دریافت اطلاعات با استفاده از http با متد GET
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectParams"></param>
    /// <param name="url"></param>
    /// <param name="contenttype"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<T> GetAsync<T>(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null);
  }
}

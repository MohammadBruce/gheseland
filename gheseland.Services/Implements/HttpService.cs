using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace gheseland.Services.Implements
{
  public class HttpService : IHttpService
  {
    public HttpService()
    {

    }

    public object Get(object objectParams, string url, string contenttype = "application/json", Dictionary<string, string> headers = null)
    {
      var qParams = "";
      if (objectParams != null)
      {
        var type = objectParams.GetType();
        var props = type.GetProperties();
        var pairs = props.Select(x => x.Name + "=" + x.GetValue(objectParams, null)).ToArray();
        qParams = string.Join("&", pairs);

      }

      var finalUrl = url + qParams;
      if (finalUrl.Substring(finalUrl.Length - 1) == "?")
      {
        finalUrl = finalUrl.Substring(finalUrl.Length - 1);
      }
      HttpClient client = new HttpClient();

      client.BaseAddress = new Uri(finalUrl);
      // Add an Accept header for JSON format.  
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contenttype));
      if (headers != null)
      {
        foreach (var header in headers)
        {
          client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
      }



      HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call!  
      if (response.IsSuccessStatusCode)
      {
        return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
      }
      else
      {
        return null;
      }
    }

    public T Get<T>(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null)
    {
      var response = Get(objectParams, url, contenttype, headers);
      return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(response));
    }
    public object Post(object objectParams, string url, string contenttype = "application/json", Dictionary<string, string> headers = null, string authorizationKey = null)
    {
      var client = new HttpClient { BaseAddress = new Uri(url) };



      // Add an Accept header for JSON format.  
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contenttype));
      if (!string.IsNullOrEmpty(authorizationKey))
      {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            AuthenticationSchemes.Basic.ToString(),
            authorizationKey);
      }

      if (headers != null)
      {
        foreach (var header in headers)
        {
          client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
      }

      var param = JsonConvert.SerializeObject(objectParams);
      var content = new StringContent(param, Encoding.UTF8, contenttype);
      var response = client.PostAsync(url, content).Result; // Blocking call!  
      if (response.IsSuccessStatusCode)
      {
        return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
      }
      else
      {
        return null;
      }
    }

    public T Post<T>(object objectParams, string url, string contenttype = "application/json", Dictionary<string, string> headers = null,
      string authorizationKey = null)
    {
      var response = Post(objectParams, url, contenttype, headers, authorizationKey);
      return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(response));
    }

    public async Task<T> GetAsync<T>(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null)
    {
      return await Task.Factory.StartNew(() =>
          Get<T>(objectParams, url, contenttype, headers)
          );
    }

    public async Task<T> PostAsync<T>(object objectParams, string url, string contenttype = "application/json",
      Dictionary<string, string> headers = null,
      string authorizationKey = null)
    {
      return await Task.Factory.StartNew(() =>
        Post<T>(objectParams, url, contenttype, headers, authorizationKey)
      );
    }
  }
}

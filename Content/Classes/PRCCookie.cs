using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    public class PRCCookie
    {
        public bool SetPrcCookie(string key, string val)
        {

            try
            {


                var baseAddress = new Uri("http://example.com");
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>(key, val),                 
                });
                    cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                    var result = client.PostAsync("/", content).Result;
                    result.EnsureSuccessStatusCode();

                    return true;

                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }


    }
}
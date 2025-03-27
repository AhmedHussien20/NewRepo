using Domain_Layer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_API.Services
{
    public class CookieHandler : PageModel
    {
        HttpResponse _response;

        public CookieHandler(HttpResponse response)
        {
            _response = response;
        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddDays(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            _response.Cookies.Append(key, value, option);
        }

        public void Remove(string key, HttpResponse response)
        {
            response.Cookies.Delete(key);
        }

        public String getCookie(CookieValues value, HttpRequest request)
        {
            String enumValue = value.ToString();
            string cookieValueFromReq = request.Cookies[enumValue];
            return cookieValueFromReq;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Crop_Management_System.Services
{
    public class EmailHandlerApi
    {
        string url = "https://swishandroid.swish.co.zm/api/SendCustomEmail";
        //string url = "https://android.swish.co.zm/api/SendCustomEmail";

        public async Task<bool> emailLogger(string email, string message, string subject)
        {
            messageBody obj = new messageBody
            {
                email = email,
                message = message,
                subject = subject
            };


            var response = await postRequestAsync(obj, url);
            if (response.IsSuccessStatusCode)
            {
                var serverResponse = await response.Content.ReadAsStringAsync();
                var serverResponseBody = JObject.Parse(serverResponse);
                //Debug.Write($"\n\nSms Response Body: {serverResponseBody}");
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<HttpResponseMessage> postRequestAsync(dynamic obj, string url)
        {
            try
            {
                string jsonBody = JsonConvert.SerializeObject(obj);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage results = await client.PostAsync(url, content);
                return results;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"postRequestError: {e.Message}");
                return null;
            }
        }

        public class messageBody
        {
            public string email { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
        }
    }
}

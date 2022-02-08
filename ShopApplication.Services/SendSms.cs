using Microsoft.Extensions.Options;
using Nancy.Json;
using ShopApplication.DataLayer;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Services
{
   public class SendSms
    {
        private IHttpClientFactory clientFactory;
        private readonly ISiteService siteService;

        public SendSms(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            
        }
        public SendSms(ISiteService siteService)
        {
            this.siteService = siteService;
        }
        
        public string Base64Encode(string plaintext)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(plainTextBytes);
        }

        public long GetTimeStamp(DateTime dt, DateTimeKind dtk)
        {
            long unixTimestamp = (long)(dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, dtk))).TotalSeconds;
            return unixTimestamp;
        }
        public class SendMessageResult
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public string Result { get; set; }
        }

        public async Task SendMessagess(string mobilenumber, string text)
        {
            var settings =await siteService.GetSiteSetting();
            List<string> str = new List<string>();
            str.Add(mobilenumber);
            object input = new
            {
                PhoneNumber = settings.SmsServiceNumber, // شماره اختصاصی
                Message = text, // متن پیام ارساالی
                UserGroupID = Guid.NewGuid(), // شماره پیگیری
                Mobiles = str, // لیست شماره موبایل ها
                SendDateInTimeStamp = new SendSms(clientFactory).GetTimeStamp(DateTime.Now, DateTimeKind.Local) // تاریخ ارسال به صورت timestamp
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);//تبدیل ورودی به json
            string res = await PostData("SendMessage", inputJson, settings.SmsServiceUserName, settings.SmsServicePassword);//فراخوانی تابع post data
            //var output = (new JavaScriptSerializer()).Deserialize<SendMessageResult>(res);//دریافت نتیجه و تبدیل به رشته
            
        }

        public async Task<string> PostData(string method, string inputJson, string UserName, string Password)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "http://smspanel.trez.ir/api/smsAPI/SendMessage/");
            //request.Headers.Add("Content-type", "application/json");
            string token = Base64Encode(UserName + ":" + Password);
            request.Headers.Add("Authorization", string.Format("Basic {0}", token));
            request.Content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            var client = clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}

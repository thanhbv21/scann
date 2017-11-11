using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scannn.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Scannn.Data
{
    public class GetUserProfile : IGetUserProfile
    {
        HttpClient client;
        UserProfile user = new UserProfile();

        public GetUserProfile()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public async Task<string> PerformGetFullNameAsync(string email)
        {
            var uri = new Uri(Constants.RestGetProfileWithoutSSUrl);
            LoginData logindata = new LoginData()
            {
                user_email = email
            };
            Debug.WriteLine("Activate get Profile");
            try
            {
                var json = JsonConvert.SerializeObject(logindata);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                Debug.WriteLine("link:" + uri);
                //var response = await client.GetAsync(uri);
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var rpcontent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(rpcontent);
                    Newtonsoft.Json.Linq.JObject fullname = new Newtonsoft.Json.Linq.JObject();
                    fullname = Newtonsoft.Json.Linq.JObject.Parse(rpcontent);
                    Debug.WriteLine("jsondata:" + fullname);
                    return (string)fullname["profile"]["fullname"];
                    //var Items = JsonConvert.DeserializeObject<LoginAPI>(rpcontent);
                    //rpcontent.
                    //return Items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
        }

        public Task<UserProfileAPI> PerformGetUserProfileAsync(string session, string mode)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginAPI> PerformLoginAsync(string email, string password)
        {
            //Debug.WriteLine(email + " " + password);
            LoginAPI Items = new LoginAPI();
            LoginData logindata = new LoginData()
            {
                user_email = email,
                user_pwd = password
            };
            var uri = new Uri(Constants.RestLoginUrl);
            Debug.WriteLine("Activate get Login");
            try
            {
                var json = JsonConvert.SerializeObject(logindata);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                Debug.WriteLine("link:" + uri);
                //var response = await client.GetAsync(uri);
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var rpcontent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(rpcontent);
                    Items = JsonConvert.DeserializeObject<LoginAPI>(rpcontent);
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
        }

        public async Task PerformLogoutAsync(string sessionid)
        {
            //Debug.WriteLine(email + " " + password);
            //LoginAPI Items = new LoginAPI();
            LoginData logindata = new LoginData()
            {
                user_session = sessionid
            };
            var uri = new Uri(Constants.RestLogoutUrl);
            Debug.WriteLine("Activate Logout");
            try
            {
                var json = JsonConvert.SerializeObject(logindata);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                Debug.WriteLine("link:" + uri);
                //var response = await client.GetAsync(uri);
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var rpcontent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(rpcontent);
                    //Items = JsonConvert.DeserializeObject<LoginAPI>(rpcontent);
                    //return Items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            //throw new NotImplementedException();
        }
    }
}

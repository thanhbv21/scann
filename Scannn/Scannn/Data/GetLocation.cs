using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scannn.Models;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Scannn.Data
{
    public class GetLocation : IGetLocation
    {
        HttpClient client;
        LocationAPI location = new LocationAPI();

        public GetLocation()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000
            };
        }
        public async Task<LocationAPI> PerformGetLocationAsync()
        {
            Debug.WriteLine("Activate get Location");
            var uri = new Uri("https://ipinfo.io/");
            try
            {
                Debug.WriteLine("link:" + uri);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);
                    location = Newtonsoft.Json.JsonConvert.DeserializeObject<LocationAPI>(content);
                    return location;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
        }

        public async Task PerformUpdateLocationAsync(LocationAPI location, bool isNewLocation, string itemcode)
        {
            Debug.WriteLine("Activate get Location");
            var uri = new Uri(string.Format(Constants.RestUpdateLocationUrl, string.Empty));

            LocationData LocalData = new LocationData()
            {
                code = itemcode,
                city = location.city,
                country = location.country,
                hostname = location.hostname,
                ip = location.ip,
                loc = location.loc,
                org = location.org,
                region = location.region,
                device = "app"
            };
            try
            {
                var json = JsonConvert.SerializeObject(LocalData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                if(isNewLocation)
                {
                    Debug.WriteLine("link:" + uri);
                    response = await client.PostAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             Location successfully saved. Status code"+ response.StatusCode+"\n"+response.RequestMessage);

                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
    }
}

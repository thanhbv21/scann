using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scannn.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Scannn.Data
{
    public class GetNews : IGetNews
    {
        HttpClient client;
        NewsItem news = new NewsItem();

        public GetNews()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 2097152,
                Timeout = TimeSpan.FromSeconds(15)
            };
        }
        public async Task<NewItemsAPI> PerformGetNewsAsync()
        {
            System.Diagnostics.Debug.WriteLine("Active get news");
            NewItemsAPI Items = new NewItemsAPI();
            var uri = new Uri(Constants.RestNewsUrl);
            try
            {
                System.Diagnostics.Debug.WriteLine("link:" + uri);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(content);
                    Items = JsonConvert.DeserializeObject<NewItemsAPI>(content);
                    //return Items;
                }
                else
                    System.Diagnostics.Debug.WriteLine("Hỏng");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }
            return Items;
            throw new HttpRequestException();
        }
    }
}

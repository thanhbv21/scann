using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scannn.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scannn.Data
{
    public class GetProduct : IGetProduct
    {
        HttpClient client;
        Product product = new Product();
        Company company = new Company();
        public GetProduct ()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public async Task PerformPurchaseProductAsync(string itemcode, bool isPurchase)
        {
            Debug.WriteLine("Activate Purchase");
            var uri = new Uri(string.Format(Constants.RestPurchaseUrl, string.Empty));
            try
            {
                var json = new JObject(new JProperty("code", itemcode));
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                if (isPurchase)
                {
                    Debug.WriteLine("link:" + uri);
                    response = await client.PostAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             Location successfully saved. Status code: " + response.StatusCode + "\n" + response.RequestMessage);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
        public async Task<CompanyAPI> PerformGetCompanyAsync(string itemcode)
        {

            Debug.WriteLine("Activate get Company");
            CompanyAPI Items = new CompanyAPI();
            Debug.WriteLine("mã hàng:" + itemcode);
            var uri = new Uri(string.Format(Constants.RestCompanyUrl, itemcode));
            try
            {
                Debug.WriteLine("link:" + uri);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);
                    Items = JsonConvert.DeserializeObject<CompanyAPI>(content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
        }

        public async Task<ImageAPI> PerformGetImageAsync(string itemcode)
        {
            Debug.WriteLine("Activate get Image");
            ImageAPI Items = new ImageAPI();
            Debug.WriteLine("mã hàng:" + itemcode);
            var uri = new Uri(string.Format(Constants.RestImageUrl, itemcode));
            try
            {
                Debug.WriteLine("link:" + uri);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);
                    Items = JsonConvert.DeserializeObject<ImageAPI>(content);
                    if(Items.code == 200)
                    return Items;
                    else
                    {
                        Items.image = "";
                        return Items;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
        }   

        public async Task<ProductAPI> PerformGetProductAsync(string itemcode)
        {
            Debug.WriteLine("Activate get Product");
            ProductAPI Items = new ProductAPI();
            Debug.WriteLine("mã hàng:" + itemcode);
            var uri = new Uri(string.Format(Constants.RestProductUrl, itemcode));
            try
            {
                Debug.WriteLine("link:" + uri);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);
                    Items = JsonConvert.DeserializeObject<ProductAPI>(content);
                    Debug.WriteLine("Sản phẩm lấy được: " + Items.item.pro_name);
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
            
        }

        public async Task<LHYDAPI> PerformGetLHYDAPI(string itemcode)
        {
            Debug.WriteLine("Activate get LHYD");
            LHYDAPI Items = new LHYDAPI();
            Debug.WriteLine("mã hàng:" + itemcode);
            var uri = new Uri(string.Format(Constants.RestLHYDUrl, itemcode));
            try
            {
                Debug.WriteLine("link:" + uri);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);
                    Items = JsonConvert.DeserializeObject<LHYDAPI>(content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}

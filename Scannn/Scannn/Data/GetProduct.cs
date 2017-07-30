using Newtonsoft.Json;
using Scannn.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
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
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Company> PerformGetCompanyAsync(string itemcode)
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
                    return Items.company;
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
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            throw new NotImplementedException();
        }   

        public async Task<Product> PerformGetProductAsync(string itemcode)
        {
            Debug.WriteLine("Activate get Product");
            ProductAPI Items = new ProductAPI();
            Product pro = new Product();
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
                    pro = Items.item;
                    Debug.WriteLine("Sản phẩm lấy được: " + pro.pro_name);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
           // throw new NotImplementedException();
            return pro;
        }
    }
}

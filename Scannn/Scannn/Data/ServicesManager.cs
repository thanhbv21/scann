using Scannn.Models;
using System.Threading.Tasks;

namespace Scannn.Data
{
    public class ServicesManager
    {
        IGetProduct getproduct;

        public ServicesManager (IGetProduct service)
        {
            getproduct = service;
        }

        public Task<ImageAPI> GetImageAsync(string itemcode)
        {
            return getproduct.PerformGetImageAsync(itemcode);
        }

        public Task<Product> GetProductAsync(string itemcode)
        {
            return getproduct.PerformGetProductAsync(itemcode);
        }

        public Task<Company> GetCompanyAsync(string itemcode)
        {
            return getproduct.PerformGetCompanyAsync(itemcode);
        }
    }
}

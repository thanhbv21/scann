using Scannn.Models;
using System.Threading.Tasks;

namespace Scannn.Data
{
    public class ServicesManager
    {
        IGetProduct getproduct;
        IGetNews getnews;
        IGetLocation getlocation;
        IGetUserProfile getuserprofile;

        public ServicesManager (IGetProduct service)
        {
            getproduct = service;
        }

        public ServicesManager(IGetLocation service)
        {
            getlocation = service;

        }
        public ServicesManager(IGetNews service)
        {
            getnews = service;
        }

        public ServicesManager(IGetUserProfile service)
        {
            getuserprofile = service;
        }

        public Task<NewItemsAPI> GetNewsAsync()
        {
            return getnews.PerformGetNewsAsync();
        }

        public Task<LocationAPI> GetLocationAsync()
        {
            return getlocation.PerformGetLocationAsync();
        }

        public Task UpdateLocationAsync(LocationAPI location, bool isNewLocation, string itemcode)
        {
            return getlocation.PerformUpdateLocationAsync(location, isNewLocation, itemcode);
        }

        public Task<ImageAPI> GetImageAsync(string itemcode)
        {
            return getproduct.PerformGetImageAsync(itemcode);
        }

        public Task<ProductAPI> GetProductAsync(string itemcode)
        {
            return getproduct.PerformGetProductAsync(itemcode);
        }

        public Task PurchaseProductAsync(string itemcode, bool isPurchase)
        {
            return getproduct.PerformPurchaseProductAsync(itemcode, isPurchase);
        }

        public Task<CompanyAPI> GetCompanyAsync(string itemcode)
        {
            return getproduct.PerformGetCompanyAsync(itemcode);
        }

        public Task<LHYDAPI> GetLHYDAsync(string itemcode)
        {
            return getproduct.PerformGetLHYDAPI(itemcode);
        }

        public Task<LoginAPI> DoLoginAsync(string email, string password)
        {
            
            return getuserprofile.PerformLoginAsync(email, password);
        }
        public Task DoLogoutAsync(string sessionid)
        {
            return getuserprofile.PerformLogoutAsync(sessionid);
        }
        public Task<string> GetFullNameWitoutSSAsync(string email)
        {
            return getuserprofile.PerformGetFullNameAsync(email);
                }
    }
}

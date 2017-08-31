using Scannn.Models;
using System.Threading.Tasks;

namespace Scannn
{
    public interface IGetProduct
    {
        Task<ProductAPI> PerformGetProductAsync(string itemcode);

        Task<CompanyAPI> PerformGetCompanyAsync(string itemcode);

        Task<ImageAPI> PerformGetImageAsync(string itemcode);

        Task PerformPurchaseProductAsync(string itemcode, bool isPurchase);
        //Task SaveTodoItemAsync(TodoItem item, bool isNewItem);

        //Task DeleteTodoItemAsync(string id);
    }
}

using Scannn.Models;
using System.Threading.Tasks;

namespace Scannn
{
    public interface IGetProduct
    {
        Task<Product> PerformGetProductAsync(string itemcode);

        Task<Company> PerformGetCompanyAsync(string itemcode);

        Task<ImageAPI> PerformGetImageAsync(string itemcode);

        //Task SaveTodoItemAsync(TodoItem item, bool isNewItem);

        //Task DeleteTodoItemAsync(string id);
    }
}

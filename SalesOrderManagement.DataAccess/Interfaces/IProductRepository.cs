using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.DataAccess.DTO;

namespace SalesOrderManagement.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task AddNewProductAsyc(Guid orderId, Window window);
        Task UpdateProductAsync(Window window, Guid windowId);
        Task DeleteAsync(Guid id);
        Task<Window> GetProductById(Guid id);
    }
}

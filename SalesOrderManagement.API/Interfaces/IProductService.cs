using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.DataAccess.DTO;

namespace SalesOrderManagement.API.Interfaces
{
    public interface IProductService
    {
        Task<Window> CreateNewProductAsync(WindowDto windowDto);
        Task AddNewProductAsync(Guid orderId, Window window);
        Task UpdateProductAsync(WindowDto windowDto, Guid windowId);
        Task DeleteProductAsync(Guid id);
    }
}

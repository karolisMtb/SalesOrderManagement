using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.DataAccess.DTO;

namespace SalesOrderManagement.API.Interfaces
{
    public interface ISalesOrderService
    {
        Task<Order> CreateNewOrderAsync(SalesOrderDto orderDto);
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<bool> DeleteOrderAsync(Guid id);
        
    }
}

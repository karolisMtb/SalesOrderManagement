using SalesManagementApp.DataAccess.Entities;

namespace SalesOrderManagement.DataAccess.Interfaces
{
    public interface ISalesOrderRepository
    {
        Task AddNewOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task DeleteOrderAsync(Order order);
        
    }
}

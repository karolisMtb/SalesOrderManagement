using Microsoft.EntityFrameworkCore;
using SalesManagementApp.DataAccess.DatabaseContext;
using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.DataAccess.Interfaces;

namespace SalesOrderManagement.DataAccess.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly SalesManagementDb _dbContext;

        public SalesOrderRepository(SalesManagementDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddNewOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders.Include(x => x.Windows).ThenInclude(x => x.SubElements).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _dbContext.Orders.Where(x => x.Id == id).Include(x => x.Windows).ThenInclude(x => x.SubElements).FirstOrDefaultAsync();
        }
    }
}

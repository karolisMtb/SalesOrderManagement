using Microsoft.EntityFrameworkCore;
using SalesManagementApp.DataAccess.DatabaseContext;
using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.DataAccess.Interfaces;

namespace SalesOrderManagement.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesManagementDb _dbContext;

        public ProductRepository(SalesManagementDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddNewProductAsyc(Guid orderId, Window window)
        {
            Order? order = await _dbContext.Orders.Where(x => x.Id == orderId).Include(x => x.Windows).ThenInclude(x => x.SubElements).FirstOrDefaultAsync();

            if (order is not null)
            {
                order.Windows.Add(window);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            Window? window = await _dbContext.Windows.Where(x => x.Id == id).Include(x => x.SubElements).FirstOrDefaultAsync();

            if (window is not null)
            {
                _dbContext.Windows.Remove(window);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<Window> GetProductById(Guid id)
        {
            return _dbContext.Windows.Where(x => x.Id == id).Include(x => x.SubElements).FirstOrDefaultAsync();
        }

        public async Task UpdateProductAsync(Window window, Guid windowId)
        {
            Window? windowToUpdate = await _dbContext.Windows.Where(x => x.Id == windowId).Include(x => x.SubElements).FirstOrDefaultAsync();

            if (windowToUpdate is null)
            {
                throw new ArgumentException("Updating required product was unsuccessfull. Please check your input and try again");
            }
            
            windowToUpdate = window;
            await _dbContext.SaveChangesAsync();
        }


    }
}

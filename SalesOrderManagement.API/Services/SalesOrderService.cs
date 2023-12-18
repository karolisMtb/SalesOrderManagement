using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.API.Exceptions;
using SalesOrderManagement.API.Interfaces;
using SalesOrderManagement.DataAccess.DTO;
using SalesOrderManagement.DataAccess.Interfaces;

namespace SalesOrderManagement.API.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public SalesOrderService(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }
        public async Task<Order> CreateNewOrderAsync(SalesOrderDto orderDto)
        {
            Order order = new Order()
            {
                Name = orderDto.name,
                State = orderDto.state,
                Windows = await GetWindowOrderAsync(orderDto.windows),
            };

            await _salesOrderRepository.AddNewOrderAsync(order);

            Order existingOrder = await _salesOrderRepository.GetOrderByIdAsync(order.Id);

            if(existingOrder is null)
            {
                throw new InvalidOperationException("Order could not be created");
            }

            return existingOrder;
        }

        private async Task<List<Window>> GetWindowOrderAsync(List<WindowDto> windowDto)
        {
            List<Window> windows = new List<Window>();

            foreach (var window in windowDto)
            {
                if(window.quantity <= 0)
                {
                    throw new ArgumentException("Product quantity cannot be negative or 0.");
                }

                windows.Add(new Window()
                {
                    Name = window.name,
                    QuantityOfWindows = window.quantity,
                    SubElements = await GetSubElementsAsync(window)
                });
            }
            return windows;
        }

        private async Task<List<SubElement>> GetSubElementsAsync(WindowDto windowDto)
        {
            List<SubElement> subElements = new List<SubElement>();

            foreach(var subElement in windowDto.subElementsDto)
            {
                if (subElement.width <= 0 || subElement.height <= 0)
                {
                    throw new ArgumentException("Window height or width cannot be negative or 0.");
                }

                subElements.Add(new SubElement()
                {
                    Element = subElement.Element,
                    Type = subElement.type,
                    Width = subElement.width,
                    Height = subElement.height
                });
            }

            return subElements;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _salesOrderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            IEnumerable<Order> orders = await _salesOrderRepository.GetAllOrdersAsync();

            if(orders.Count() == 0 || orders is null)
            {
                throw new ObjectNotFoundException("There are no results.");
            }

            return orders;
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            Order order = await _salesOrderRepository.GetOrderByIdAsync(id);
            
            if(order is null)
            {
                throw new ObjectNotFoundException("No orders have been found that you are lookiing for.");
            }

            await _salesOrderRepository.DeleteOrderAsync(order);

            return (await _salesOrderRepository.GetOrderByIdAsync(id) is null) ? true : false;
        }

        
    }
}

using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.API.Interfaces;
using SalesOrderManagement.DataAccess.DTO;
using SalesOrderManagement.DataAccess.Interfaces;

namespace SalesOrderManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Window> CreateNewProductAsync(WindowDto windowDto)
        {
            return await GetProductAsync(windowDto);
        }

        public async Task AddNewProductAsync(Guid orderId, Window window)
        {
            await _productRepository.AddNewProductAsyc(orderId, window);
        }

        private async Task<List<SubElement>> GetSubElementsAsync(WindowDto windowDto)
        {
            List<SubElement> subElements = new List<SubElement>();

            foreach (var subElement in windowDto.subElementsDto)
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

        private async Task<Window> GetProductAsync(WindowDto windowDto)
        {
            if (windowDto.quantity <= 0)
            {
                throw new ArgumentException("Product quantity cannot be negative or 0.");
            }

            return new Window()
            {
                Name = windowDto.name,
                QuantityOfWindows = windowDto.quantity,
                SubElements = await GetSubElementsAsync(windowDto)
            };
        }

        public async Task UpdateProductAsync(WindowDto windowDto, Guid windowId)
        {
            Window window = await GetProductAsync(windowDto);

            await _productRepository.UpdateProductAsync(window, windowId);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}

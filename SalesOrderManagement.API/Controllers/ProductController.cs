using Microsoft.AspNetCore.Mvc;
using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.API.Exceptions;
using SalesOrderManagement.API.Interfaces;
using SalesOrderManagement.DataAccess.DTO;

namespace SalesOrderManagement.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Adds new product
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddNewProductAsync([FromBody] WindowDto windowDto, [FromQuery] Guid orderId)
        {
            if(windowDto is null || orderId == Guid.Empty) 
            {
                return BadRequest("Please check your input values. Cannot be empty");
            }

            try
            {
                Window product = await _productService.CreateNewProductAsync(windowDto);

                await _productService.AddNewProductAsync(orderId, product);

                return Ok("New product is successfully added.");
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Client side error: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Server side error: {e.Message}");
            }
        }

        /// <summary>
        /// Updates existing product
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch]
        public async Task<IActionResult> ModifyProdductAsync([FromBody] WindowDto windowDto, [FromQuery] Guid windowId)
        {
            if(windowDto is null || windowId == Guid.Empty)
            {
                return BadRequest("Wrong input");
            }

            try
            {
                await _productService.UpdateProductAsync(windowDto, windowId);
                return Ok("Product was succesfully updated.");
            }
            catch (ObjectNotFoundException e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Client side error: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Server side error: {e.Message}");
            }
        }

        /// <summary>
        /// Deletes current product
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            try
            {
                if(id == Guid.Empty)
                {
                    return BadRequest("Please check input data and try again.");
                }

                await _productService.DeleteProductAsync(id);
                return Ok("Product was successfully deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Product could not be deleted: {e.Message}");
            }
        }
    }
}

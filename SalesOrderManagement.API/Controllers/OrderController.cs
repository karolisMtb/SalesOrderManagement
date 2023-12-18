using Microsoft.AspNetCore.Mvc;
using SalesManagementApp.DataAccess.Entities;
using SalesOrderManagement.API.Exceptions;
using SalesOrderManagement.API.Interfaces;
using SalesOrderManagement.DataAccess.DTO;

namespace SalesOrderManagement.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ISalesOrderService salesOrderService, ILogger<OrderController> logger)
        {
            _salesOrderService = salesOrderService;
            _logger = logger;
        }

        /// <summary>
        /// Returns all available orders
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            //only iterate and read-only access
            try
            {
                IEnumerable<Order> orders = await _salesOrderService.GetAllAsync();

                return Ok(orders);
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
        /// Creates new window order
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateNewOrderAsync([FromBody] SalesOrderDto newOrder)
        {
            //TODO
            //by default string stype is a placeholder and 0 for int. Those must be validated!

            try
            {
                if (newOrder is null)
                {
                    return BadRequest("Order cannot be empty");
                }

                Order order = await _salesOrderService.CreateNewOrderAsync(newOrder);
                return Ok(order);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Client side error: {e.Message}");
            }
            catch(ArgumentException e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"An error occured: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Server side error: {e.Message}");
            }
        }

        /// <summary>
        /// Modifies existing order and updates
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
        public async Task<IActionResult> EditExistingOrderAsync([FromBody] Order order)
        {
            if (order is null)
            {
                return BadRequest("bla");
            }

            return Ok(order);
        }

        /// <summary>
        /// Returns order based on ID
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetOrderByIdAsync([FromQuery] Guid id)
        {
            if(Guid.Empty.Equals(id))
            {
                return BadRequest("Wrong input");
            }

            try
            {
                Order order = await _salesOrderService.GetOrderByIdAsync(id);

                if(order is not null)
                {
                    return Ok(order);
                }

                return BadRequest($"Somethin went wrong.");
            }
            catch (ObjectNotFoundException e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Client side error occured: {e.Message}");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Server side error occured: {e.Message}");
            }
        }

        /// <summary>
        /// Deletes order based on ID
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderAsync([FromQuery] Guid id)
        {
            try
            {
                var result = await _salesOrderService.DeleteOrderAsync(id);
               
                if(result is true)
                {
                    return Ok("Order is deleted successfully.");
                }

                return BadRequest("Order could not be deleted. ");
            }
            catch (ObjectNotFoundException e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Client side error occured: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Server side error occured: {e.Message}");
            }
        }

    }
}

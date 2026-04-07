using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Commands;
using OrderService.Dtos;
using OrderService.Queries;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderCommandHandler _createOrderCommandHandler;
        private readonly GetOrderByIdQueryHandler _getOrderByIdQueryHandler;
        private readonly GetAllOrdersQueryHandler _getAllOrdersQueryHandler;
        public OrdersController(CreateOrderCommandHandler createOrderCommandHandler, GetOrderByIdQueryHandler getOrderByIdQueryHandler, GetAllOrdersQueryHandler getAllOrdersQueryHandler)
        {
            _createOrderCommandHandler = createOrderCommandHandler;
            _getOrderByIdQueryHandler = getOrderByIdQueryHandler;
            _getAllOrdersQueryHandler = getAllOrdersQueryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerEmail))
            {
                return BadRequest("Customer email is required.");
            }

            if (request.TotalAmount <= 0)
            { 
            return BadRequest("Total amount must be greater than zero.");
            }

            var command = new CreateOrderCommand
            {
                CustomerEmail = request.CustomerEmail,
                TotalAmount = request.TotalAmount,
            };

            var order = await _createOrderCommandHandler.HandleAsync(command);
            return Ok(new
            {
                order.Id,
                order.CustomerEmail,
                order.TotalAmount,
                order.Status,
                order.CreatedAtUtc,
                order.CorrelationId
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        { 
         var query = new GetOrderByIdQuery { Id = id };

            var order = await _getOrderByIdQueryHandler.HandleAsync(query);

            if (order == null)
            { 
            return NotFound();
            }

            return Ok(order);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0) { 
                return BadRequest("Page Number must be greater thaen zero");
            }

            var query = new GetAllOrdersQuery { PageNumber = pageNumber , PageSize= pageSize };

            var result = await _getAllOrdersQueryHandler.HandleAsync(query);
            return Ok(result);
        }

    }
}

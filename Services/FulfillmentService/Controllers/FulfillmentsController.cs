using FulfillmentService.Commands;
using FulfillmentService.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FulfillmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FulfillmentsController : ControllerBase
    {
        private readonly GetFulfillmentByOrderQueryHandler _handler;
        private readonly CreateFulfillmentCommandHandler _commandHandler;

        public FulfillmentsController(GetFulfillmentByOrderQueryHandler handler,CreateFulfillmentCommandHandler commadHandler) {
            _handler = handler;
            _commandHandler = commadHandler;
        }
    
    [HttpGet("by-order/{orderId:guid}")]
        public async Task<IActionResult> GetByOrder(Guid orderId) {
            var query = new GetFulfillmentByOrderIdQuery
            {
                OrderId = orderId,
            };

            var result = await _handler.HandleAsync(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFulfillment(CreateFulfillmentCommand command)
        {
            var result = await _commandHandler.HandleAsync(command);
            return Ok(result);
        }
    }
}

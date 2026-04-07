using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Commands;
using PaymentService.Queries;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly GetPaymentByOrderIdQueryHandler _getPaymentByOrderIdQueryHandler;
        private readonly ProcessPaymentCommandHandler _handler;

        public PaymentController(GetPaymentByOrderIdQueryHandler getPaymentByOrderIdQueryHandler, ProcessPaymentCommandHandler handler)
        {
            _getPaymentByOrderIdQueryHandler = getPaymentByOrderIdQueryHandler;
            _handler = handler;
        }

        [HttpGet("by-order/{orderId:guid}")]
        public async Task<IActionResult> GetPaymentByOrderId(Guid orderId)
        {
            var query = new GetPaymentByOrderIdQuery
            {
                OrderId = orderId
            };

            var payment = await _getPaymentByOrderIdQueryHandler.HandleAsync(query);

            if (payment == null)
            { 
            return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment(ProcessPaymentCommand command)
        {
            var result = await _handler.HandleAsync(command);
            return Ok(result);
        }
    }
}

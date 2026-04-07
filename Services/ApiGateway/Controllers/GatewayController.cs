using ApiGateway.Dtos;
using ApiGateway.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly GatewayService _gatewayService;

        public GatewayController(GatewayService gatewayservice) { 
        _gatewayService = gatewayservice;
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderGatewayRequestDto request) {
            var response = await _gatewayService.CreateOrderAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) {
                return StatusCode((int)response.StatusCode, content);
            }

            return Content(content, "application/json");
        }

        [HttpGet("orders/{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                var result = await _gatewayService.GetOrderAsync(id);
                return Content(result ?? string.Empty, "application/json");
            }

            catch { 
            return NotFound();
            }
        
        }

        [HttpGet("payments/by-order/{orderId:guid}")]
        public async Task<IActionResult> GetPaymentByOrder(Guid orderId)
        {
            try
            {
                var result = await _gatewayService.GetPaymentByOrderAsync(orderId);
                return Content(result ?? string.Empty, "application/json");
            }

            catch {
                return NotFound();
            }
        }

        [HttpGet("fulfillments/by-order/{orderId:guid}")]
        public async Task<IActionResult> GetFulfillmentByOrder(Guid orderId)
        {
            try
            {
                var result = await _gatewayService.GetFulfillmentByOrderAsync(orderId);
                return Content(result ?? string.Empty, "application/json");
            }
            catch 
            {
            return NotFound();
            }
        }

        [HttpGet("workflow/order-summmary/{orderId:guid}")]
        public async Task<IActionResult> GetOrderWorkflowSummary(Guid orderId)
        { 
         var result = await _gatewayService.GetOrderWorkflowSummaryAsync(orderId);
            return Ok(result);
        }
    }
}

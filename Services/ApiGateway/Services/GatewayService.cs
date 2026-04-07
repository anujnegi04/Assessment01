using ApiGateway.Dtos;
using System.Security.Cryptography.X509Certificates;

namespace ApiGateway.Services
{
    public class GatewayService
    {
        private readonly HttpClient _httpClient;

        public GatewayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage>CreateOrderAsync(object request)
        {
            return await _httpClient.PostAsJsonAsync(
                "https://localhost:7179/api/orders",request);
        }
        public async Task<string?>GetOrderAsync(Guid orderId)
        {
            return await _httpClient.GetStringAsync(
                $"https://localhost:7179/api/orders/{orderId}");
        }
        public async Task<string?>GetPaymentByOrderAsync(Guid orderId)
        {
            return await _httpClient.GetStringAsync(
               $"https://localhost:7226/api/payment/by-order/{orderId}" );
        }
        public async Task<string?>GetFulfillmentByOrderAsync(Guid orderId)
        {
            return await _httpClient.GetStringAsync(
                $"https://localhost:7253/api/fulfillments/by-order/{orderId}");
        }

        public async Task<OrderWorkflowSummaryDto> GetOrderWorkflowSummaryAsync(Guid orderId)
        {
            var result = new OrderWorkflowSummaryDto();
            try
            {
                result.Order = await GetOrderAsync(orderId);
            }
            catch {
                result.Order = null;
            }

            try
            {
                result.Payment = await GetPaymentByOrderAsync(orderId);
            }
            catch {
                result.Payment = null;
            }

            try
            {
                result.Fulfillment = await GetFulfillmentByOrderAsync(orderId);
            }
            catch {
                result.Fulfillment = null;
            }

            return result;
        }
    }
}

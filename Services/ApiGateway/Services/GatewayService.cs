using ApiGateway.Dtos;
using System.Security.Cryptography.X509Certificates;

namespace ApiGateway.Services
{
    public class GatewayService
    {
        private readonly HttpClient _httpClient;
        private readonly string _orderServiceUrl;
        private readonly string _paymentServiceUrl;
        private readonly string _fulfillmentServiceUrl;

        public GatewayService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _orderServiceUrl = configuration["ServiceUrls:OrderService"]
                ?? throw new InvalidOperationException("ServiceUrls:OrderService is not configured.");
            _paymentServiceUrl = configuration["ServiceUrls:PaymentService"]
                ?? throw new InvalidOperationException("ServiceUrls:PaymentService is not configured.");
            _fulfillmentServiceUrl = configuration["ServiceUrls:FulfillmentService"]
                ?? throw new InvalidOperationException("ServiceUrls:FulfillmentService is not configured.");
        }

        public async Task<HttpResponseMessage> CreateOrderAsync(object request)
        {
            return await _httpClient.PostAsJsonAsync(
                $"{_orderServiceUrl}/api/orders", request);
        }

        public async Task<string?> GetOrderAsync(Guid orderId)
        {
            return await _httpClient.GetStringAsync(
                $"{_orderServiceUrl}/api/orders/{orderId}");
        }

        public async Task<string?> GetPaymentByOrderAsync(Guid orderId)
        {
            return await _httpClient.GetStringAsync(
                $"{_paymentServiceUrl}/api/payment/by-order/{orderId}");
        }

        public async Task<string?> GetFulfillmentByOrderAsync(Guid orderId)
        {
            return await _httpClient.GetStringAsync(
                $"{_fulfillmentServiceUrl}/api/fulfillments/by-order/{orderId}");
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

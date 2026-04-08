using OrderService.Data.Entities;
using OrderService.Dtos;

namespace OrderService.Services
{
    public class OrderSagaOrchestrator
    {
        private readonly HttpClient _httpClient;
        private readonly string _paymentServiceUrl;
        private readonly string _fulfillmentServiceUrl;

        public OrderSagaOrchestrator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _paymentServiceUrl = configuration["ServiceUrls:PaymentService"]
                ?? throw new InvalidOperationException("ServiceUrls:PaymentService is not configured.");
            _fulfillmentServiceUrl = configuration["ServiceUrls:FulfillmentService"]
                ?? throw new InvalidOperationException("ServiceUrls:FulfillmentService is not configured.");
        }

        public async Task ProcessOrderAsync(Order order)
        {
            var payment = await ProcessPaymentAsync(order);

            await CreateFulfillmentAsync(order, payment.Id);
        }

        private async Task<PaymentResultDto> ProcessPaymentAsync(Order order)
        {
            var paymentRequest = new
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                CorrelationId = order.CorrelationId
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"{_paymentServiceUrl}/api/payment/process", paymentRequest);

            response.EnsureSuccessStatusCode();

            var payment = await response.Content.ReadFromJsonAsync<PaymentResultDto>();

            return payment ?? throw new InvalidOperationException("Payment response was null.");
        }

        private async Task CreateFulfillmentAsync(Order order, Guid paymentId)
        {
            var fulfillmentRequest = new
            {
                OrderId = order.Id,
                PaymentId = paymentId,
                CorrelationId = order.CorrelationId
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"{_fulfillmentServiceUrl}/api/fulfillments/create", fulfillmentRequest);

            response.EnsureSuccessStatusCode();
        }
    }
}

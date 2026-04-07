using OrderService.Data.Entities;

namespace OrderService.Services
{
    public class OrderSagaOrchestrator
    {
        private readonly HttpClient _httpClient;

        public OrderSagaOrchestrator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ProcessOrderAsync(Order order) {
            var paymentRequest = new
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                CorrelationId = order.CorrelationId
            };

          var response =  await _httpClient.PostAsJsonAsync(
                "https://localhost:7226/api/payment/process", paymentRequest
                );

            response.EnsureSuccessStatusCode();
        }
    }
}

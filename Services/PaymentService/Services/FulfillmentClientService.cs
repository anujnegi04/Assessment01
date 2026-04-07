using PaymentService.Data.Entities;

namespace PaymentService.Services
{
    public class FulfillmentClientService
    {
        private readonly HttpClient _httpClient;

        public FulfillmentClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFulfillmentAsync(Payment payment) {
            var fulfillmentRequest = new
            {
                OrderId = payment.OrderId,
                PaymentId = payment.Id,
                CorrelationId = payment.CorrelationId
            };

            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7253/api/fulfillments/create", fulfillmentRequest
                );

            response.EnsureSuccessStatusCode();
        }   
    }
}

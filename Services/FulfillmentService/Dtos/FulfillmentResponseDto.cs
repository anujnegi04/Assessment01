namespace FulfillmentService.Dtos
{
    public class FulfillmentResponseDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CorrelationId { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set;  }
    }
}

namespace PaymentService.Dtos
{
    public class PaymentResponseDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set;}
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ProcessedAtUtc { get; set; }
        public string CorrelationId { get; set; } = string.Empty;
    }
}

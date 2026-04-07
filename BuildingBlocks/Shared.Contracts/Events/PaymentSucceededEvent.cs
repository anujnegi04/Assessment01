namespace Shared.Contracts.Events
{
    public class PaymentSucceededEvent
    {
        public Guid OrderId { get; set; }
        public Guid PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProcessedAtUtc { get; set; }
        public string CorrelationId { get; set; }
    }
}

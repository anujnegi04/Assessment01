namespace Shared.Contracts.Events
{
    public class PaymentFailedEvent
    {
        public Guid OrderId { get; set; }
        public string Reason { get; set; }
        public DateTime FailedAtUtc { get; set; }
        public string CorrelationId { get; set; }
    }
}

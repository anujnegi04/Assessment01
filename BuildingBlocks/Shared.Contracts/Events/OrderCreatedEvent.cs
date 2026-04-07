namespace Shared.Contracts.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerEmail { get; set; } 
        public DateTime CreatedAtUtc { get; set; }
        public string CorrelationId { get; set; }
    }
}

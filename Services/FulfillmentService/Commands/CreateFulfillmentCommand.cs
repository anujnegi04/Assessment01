namespace FulfillmentService.Commands
{
    public class CreateFulfillmentCommand
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string CorrelationId { get; set; } = string.Empty;
    }
}

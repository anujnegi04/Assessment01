namespace PaymentService.Commands
{
    public class ProcessPaymentCommand
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string CorrelationId { get; set; } = string.Empty;
    }
}

namespace OrderService.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAtUtc { get; set; }
        public string CorrelationId { get; set; } = string.Empty;
    }
}

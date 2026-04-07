namespace OrderService.Dtos
{
    public class OrderResponseDto
    {
        public Guid Id { get;set;}
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
        public string CorrelationId { get; set; } = string.Empty;
    }
}

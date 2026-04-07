namespace OrderService.Dtos
{
    public class CreateOrderRequest
    {
        public string CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

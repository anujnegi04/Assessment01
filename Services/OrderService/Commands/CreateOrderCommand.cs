namespace OrderService.Commands
{
    public class CreateOrderCommand
    {
        public string CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

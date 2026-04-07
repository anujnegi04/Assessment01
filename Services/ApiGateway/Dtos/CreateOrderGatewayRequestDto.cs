namespace ApiGateway.Dtos
{
    public class CreateOrderGatewayRequestDto
    {
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}

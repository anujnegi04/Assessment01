namespace ApiGateway.Dtos
{
    public class OrderWorkflowSummaryDto
    {
        public object? Order { get; set; }
        public object? Payment { get; set; }
        public object? Fulfillment { get; set; }
    }
}

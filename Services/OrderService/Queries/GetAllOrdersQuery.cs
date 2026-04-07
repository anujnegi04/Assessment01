namespace OrderService.Queries
{
    public class GetAllOrdersQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

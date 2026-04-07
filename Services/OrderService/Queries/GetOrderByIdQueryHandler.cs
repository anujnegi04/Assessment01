using Dapper;
using Microsoft.Data.SqlClient;
using OrderService.Dtos;

namespace OrderService.Queries
{
    public class GetOrderByIdQueryHandler
    {
        private readonly IConfiguration _configuration;

        public GetOrderByIdQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<OrderResponseDto> HandleAsync(GetOrderByIdQuery query)
        {
            var connectionString = _configuration.GetConnectionString("OrderDb");

            using var connection = new SqlConnection(connectionString);

            const string sql = @"Select Id,CustomerEmail,TotalAmount,Status,CreatedAtUtc,CorrelationId FROM Orders Where Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<OrderResponseDto>(sql, new { query.Id });
        }
    }
}

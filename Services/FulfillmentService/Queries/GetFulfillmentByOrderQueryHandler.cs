using FulfillmentService.Dtos;
using Microsoft.Data.SqlClient;
using Dapper;

namespace FulfillmentService.Queries
{
    public class GetFulfillmentByOrderQueryHandler
    {
        private readonly IConfiguration _configuration;

        public GetFulfillmentByOrderQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<FulfillmentResponseDto?> HandleAsync(GetFulfillmentByOrderIdQuery query)
        {
            var connectionString = _configuration.GetConnectionString("FulfillmentDb");

            using var connection = new SqlConnection(connectionString);

            const string sql = @"SELECT Id,OrderId,PaymentId,Status,CorrelationId,CreatedAtUtc FROM Fulfillments WHERE OrderId = @OrderId";

            return await connection.QueryFirstOrDefaultAsync<FulfillmentResponseDto>(sql, new { query.OrderId });
        
        }
    }
}

using Dapper;
using Microsoft.Data.SqlClient;
using PaymentService.Dtos;

namespace PaymentService.Queries
{
    public class GetPaymentByOrderIdQueryHandler
    {
        private readonly IConfiguration _configuration;

        public GetPaymentByOrderIdQueryHandler (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PaymentResponseDto?> HandleAsync(GetPaymentByOrderIdQuery query)
        {
            var connectionString = _configuration.GetConnectionString("PaymentDb");
            using var connection = new SqlConnection(connectionString);

            const string sql = @"SELECT Id,OrderId,Amount,Status,CorrelationId,CreatedAtUtc,ProcessedAtUtc FROM Payments WHERE OrderId = @OrderId";

            return await connection.QueryFirstOrDefaultAsync<PaymentResponseDto>(sql, new { query.OrderId });
        }
    }
}

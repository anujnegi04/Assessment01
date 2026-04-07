using Dapper;
using Microsoft.Data.SqlClient;
using OrderService.Dtos;

namespace OrderService.Queries
{
    public class GetAllOrdersQueryHandler
    {
        private readonly IConfiguration _configuration;

        public GetAllOrdersQueryHandler(IConfiguration configuration) { 
        _configuration = configuration;
        }

        public async Task<PagedResultDto<OrderResponseDto>> HandleAsync(GetAllOrdersQuery query)
        {
            var connectionString = _configuration.GetConnectionString("OrderDb");

            using var connection = new SqlConnection(connectionString);

            var offset = (query.PageNumber - 1) * query.PageSize; ;

            const string countSql = @"SELECT COUNT(*) FROM Orders";

            const string dataSql = @"SELECT Id,CustomerEmail,TotalAmount,Status,CreatedAtUtc,CorrelationId FROM Orders
                ORDER BY CreatedAtUtc DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            var totalCount = await connection.ExecuteScalarAsync<int>(countSql);

            var items = await connection.QueryAsync<OrderResponseDto>(
                dataSql, new {
                    Offset = offset,
                    PageSize = query.PageSize,
                });

            return new PagedResultDto<OrderResponseDto>
            {
                Items = items.ToList(),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };
           }
        }
    }


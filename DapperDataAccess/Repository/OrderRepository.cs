using Dapper;
using Domain.DataAccess;
using Domain.Dto;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DapperDataAccess.Repository
{
  public class OrderRepository : IOrderRepository
  {
    private readonly string connectionString;
    public OrderRepository(string connectionString) 
    {
      this.connectionString = connectionString;
    }
    public async Task<OrderDto> GetByIdAsync(int id)
    {
      const string sql = @"
            SELECT
                Id,
                CustomerId,
                TotalAmount,
                Status,
                CreatedAt,
                UpdatedAt
            FROM Orders
            WHERE Id = @Id";

      using (IDbConnection connection =
             new SqlConnection(connectionString))
      {
        return await connection.QuerySingleOrDefaultAsync<OrderDto>(
            sql,
            new { Id = id });
      }
    }
  }
}

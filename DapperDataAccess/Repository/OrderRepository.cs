using Dapper;
using Domain.DataAccess;
using Domain.Dto;
using System.Collections.Generic;
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

    public async Task<int> DeleteAsync(int id)
    {
      string sql = @"
        DELETE FROM Orders
        WHERE Id = @Id";

      using (var connection = new SqlConnection(connectionString))
      {
        return await connection.ExecuteAsync(
            sql,
            new { Id = id });
      }
    }

    public async Task<IEnumerable<OrderDto>> Get()
    {
      string sql = @"
            SELECT
                Id,
                CustomerId,
                TotalAmount,
                Status,
                CreatedAt,
                UpdatedAt
            FROM Orders";
      using (IDbConnection connection =
             new SqlConnection(connectionString))
      {
        return await connection.QueryAsync<OrderDto>(
            sql);
      }
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
      string sql = @"
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

    public async Task<int> InsertAsync(OrderDto dto)
    {
      string sql = @"
        INSERT INTO Orders
        (
            CustomerId,
            TotalAmount,
            Status,
            CreatedAt,
            UpdatedAt
        )
        OUTPUT INSERTED.Id
        VALUES
        (
            @CustomerId,
            @TotalAmount,
            @Status,
            @CreatedAt,
            @UpdatedAt
        )";

      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        return await connection.ExecuteScalarAsync<int>(
            sql,
            dto);
      }
    }

    public async Task<int> UpdateAsync(OrderDto dto)
    {
      string sql = @"
        UPDATE Orders
        SET
            CustomerId = @CustomerId,
            TotalAmount = @TotalAmount,
            Status = @Status,
            UpdatedAt = @UpdatedAt
        WHERE Id = @Id";

      using (var connection = new SqlConnection(connectionString))
      {
        return await connection.ExecuteAsync(sql, dto);
      }
    }
  }
}

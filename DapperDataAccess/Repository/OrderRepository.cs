using Dapper;
using DapperDataAccess.Connection;
using Domain.DataAccess;
using Domain.Dto;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DapperDataAccess.Repository
{
  public class OrderRepository : IOrderRepository
  {
    private readonly SqlConnectionFactory connectionFactory;
    public OrderRepository(SqlConnectionFactory connectionFactory)
    {
      this.connectionFactory = connectionFactory;
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
      string sql = @"
        DELETE FROM Orders
        WHERE Id = @Id";

      using (var connection = connectionFactory.Create())
      {
        await connection.OpenAsync(cancellationToken);
        using (var transaction = connection.BeginTransaction())
        {
          try
          {
            CommandDefinition command = new CommandDefinition(
            sql,
            new { Id = id },
            cancellationToken: cancellationToken);

            var result = await connection.ExecuteAsync(command);
            transaction.Commit();
            return result;
          }
          catch
          {
            transaction.Rollback();
            throw;
          }
        }
      }
    }

    public async Task<IEnumerable<OrderDto>> Get(CancellationToken cancellationToken)
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
      using (IDbConnection connection = connectionFactory.Create())
      {
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken);

        return await connection.QueryAsync<OrderDto>(command);
      }
    }

    public async Task<OrderDto> GetByIdAsync(int id, CancellationToken cancellationToken)
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

      using (IDbConnection connection = connectionFactory.Create())
      {
        var command = new CommandDefinition(
            sql,
            new { Id = id },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<OrderDto>(command);
      }
    }

    public async Task<int> InsertAsync(OrderDto dto, CancellationToken cancellationToken)
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

      using (var connection = connectionFactory.Create())
      {
        await connection.OpenAsync(cancellationToken);

        using (var transaction = connection.BeginTransaction())
        {
          try
          {
            var command = new CommandDefinition(
           sql,
           dto,
           cancellationToken: cancellationToken);

            var result = await connection.ExecuteScalarAsync<int>(command);
            transaction.Commit();
            return result;
          }
          catch
          {
            transaction.Rollback();
            throw;
          }
        }
      }
    }

    public async Task<int> UpdateAsync(OrderDto dto, CancellationToken cancellationToken)
    {
      string sql = @"
        UPDATE Orders
        SET
            CustomerId = @CustomerId,
            TotalAmount = @TotalAmount,
            Status = @Status,
            UpdatedAt = @UpdatedAt
        WHERE Id = @Id";

      using (var connection = connectionFactory.Create())
      {
        await connection.OpenAsync(cancellationToken);

        using (var transaction = connection.BeginTransaction())
        {
          try
          {
            var command = new CommandDefinition(
           sql,
           dto,
           cancellationToken: cancellationToken);

            var result = await connection.ExecuteAsync(command);
            transaction.Commit();
            return result;
          }
          catch
          {
            transaction.Rollback();
            throw;
          }
        }
      }
    }
  }
}

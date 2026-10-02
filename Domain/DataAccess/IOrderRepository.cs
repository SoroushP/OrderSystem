using Domain.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
  public interface IOrderRepository : IOrderReaderRepository
  {
    Task<int> InsertAsync(OrderDto dto, CancellationToken cancellationToken);

    Task<int> UpdateAsync(OrderDto dto, CancellationToken cancellationToken);

    Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
  }
}

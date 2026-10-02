using Domain.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.DataService
{
  public interface IOrderService
  {
    Task<IEnumerable<OrderDto>> Get(CancellationToken cancellationToken);
    Task<OrderDto> Get(int id, CancellationToken cancellationToken);
    Task<int> InsertAsync(OrderDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(OrderDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
  }
}

using Domain.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
  public interface IOrderReaderRepository
  {
    Task<OrderDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderDto>> Get(CancellationToken cancellationToken);
  }
}

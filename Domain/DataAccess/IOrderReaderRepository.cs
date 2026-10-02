using Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
  public interface IOrderReaderRepository
  {
    Task<OrderDto> GetByIdAsync(int id);
    Task<IEnumerable<OrderDto>> Get();
  }
}

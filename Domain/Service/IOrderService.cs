using Domain.Dto;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataService
{
  public interface IOrderService
  {
    Task<IEnumerable<OrderDto>> Get();
    Task<OrderDto> Get(int id);
    Task<int> InsertAsync(OrderDto dto);
    Task<bool> UpdateAsync(OrderDto dto);
    Task<bool> DeleteAsync(int id);
  }
}

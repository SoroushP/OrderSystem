using Domain.Dto;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
  public interface IOrderRepository : IOrderReaderRepository
  {
    Task<int> InsertAsync(OrderDto dto);

    Task<int> UpdateAsync(OrderDto dto);

    Task<int> DeleteAsync(int id);
  }
}

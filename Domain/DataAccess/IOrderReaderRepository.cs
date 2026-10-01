using Domain.Dto;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
  public interface IOrderReaderRepository
  {
    Task<OrderDto> GetByIdAsync(int id);
  }
}

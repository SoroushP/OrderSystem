using Domain.Dto;
using System.Threading.Tasks;

namespace Domain.DataService
{
  public interface IOrderService
  {
    Task<OrderDto> Get(int id);
  }
}

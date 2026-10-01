
using Domain.DataAccess;
using Domain.DataService;
using Domain.Dto;
using System.Threading.Tasks;

namespace Service
{
  public class OrderService : IOrderService
  {
    private readonly IOrderRepository orderRepository;
    public OrderService(IOrderRepository orderRepository) 
    {
      this.orderRepository = orderRepository;
    }
    public async Task<OrderDto> Get(int id)
    {
      return await orderRepository.GetByIdAsync(id);
    }
  }
}

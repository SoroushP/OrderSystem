
using Domain.DataAccess;
using Domain.DataService;
using Domain.Dto;
using Domain.Enumeration;
using System;
using System.Collections.Generic;
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

    public async Task<bool> DeleteAsync(int id)
    {
      var affectedRows = await orderRepository.DeleteAsync(id);
      return affectedRows > 0;
    }

    public async Task<OrderDto> Get(int id)
    {
      return await orderRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<OrderDto>> Get()
    {
      return await orderRepository.Get();
    }

    public async Task<int> InsertAsync(OrderDto dto)
    {
      if (dto == null)
      {
        throw new ArgumentNullException(nameof(dto));
      }
      if (dto.TotalAmount < 0)
      {
        throw new ArgumentException(
            "Total amount cannot be negative.",
            nameof(dto.TotalAmount));
      }
      dto.Status = OrderStatus.Pending;
      dto.CreatedAt = DateTime.UtcNow;
      dto.UpdatedAt = dto.CreatedAt;
      return await orderRepository.InsertAsync(dto);
    }

    public async Task<bool> UpdateAsync(OrderDto dto)
    {
      if (dto == null)
      { 
      throw new ArgumentNullException(nameof(dto));
      }
      if (dto.TotalAmount < 0)
      {
        throw new ArgumentException(
            "Total amount cannot be negative.",
            nameof(dto.TotalAmount));
      }
      dto.UpdatedAt = DateTime.UtcNow;
      var affectedRows = await orderRepository.UpdateAsync(dto);
      return affectedRows > 0;

    }
  }
}


using Domain.DataAccess;
using Domain.DataService;
using Domain.Dto;
using Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading;
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

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
      var affectedRows = await orderRepository.DeleteAsync(id, cancellationToken);
      return affectedRows > 0;
    }

    public async Task<OrderDto> Get(int id, CancellationToken cancellationToken)
    {
      if (id == default(int))
      {
        throw new BusinessException(
                "INVALID_AMOUNT",
                "id must be greater than zero.");
      }
      return await orderRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<OrderDto>> Get(CancellationToken cancellationToken)
    {
      return await orderRepository.Get(cancellationToken);
    }

    public async Task<int> InsertAsync(OrderDto dto, CancellationToken cancellationToken)
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
      return await orderRepository.InsertAsync(dto, cancellationToken);
    }

    public async Task<bool> UpdateAsync(OrderDto dto, CancellationToken cancellationToken)
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
      var affectedRows = await orderRepository.UpdateAsync(dto, cancellationToken);
      return affectedRows > 0;

    }
  }
}

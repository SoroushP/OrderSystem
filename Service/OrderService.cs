
using Domain.DataAccess;
using Domain.DataService;
using Domain.Dto;
using Domain.Enumeration;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
  public class OrderService : IOrderService
  {
    private readonly IAppLogger logger;
    private readonly ICacheService cacheService;
    private readonly IOrderRepository orderRepository;
    private const string OrderCacheKeyPrefix = "order:";

    public OrderService(IOrderRepository orderRepository, IAppLogger logger, ICacheService cacheService) 
    {
      this.logger = logger;
      this.cacheService = cacheService;
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
        BusinessException businessException = new BusinessException(
                "INVALID_AMOUNT",
                "id must be greater than zero.");
        logger.Error(businessException, "id must be greater than zero");
        throw businessException;
      }
      string cacheKey = GetCacheKey(id);
      var cachedOrder = await cacheService.GetAsync<OrderDto>(cacheKey);
      if (cachedOrder != null)
      {
        logger.Information("Order retrieved from cache. OrderId: {OrderId}", id);
        return cachedOrder;
      }
      logger.Information("try to get by id");
      var result = await orderRepository.GetByIdAsync(id, cancellationToken);
      await cacheService.SetAsync(
        cacheKey,
        result,
        TimeSpan.FromMinutes(5));
      logger.Information("Order retrieved from database and cached. OrderId: {OrderId}", id);
      return result;
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

    private string GetCacheKey(int id)
    {
      return $"{OrderCacheKeyPrefix}{id}";
    }
  }
}

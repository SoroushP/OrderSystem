using Domain.DataModel;
using Domain.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Service.Mapping
{
  public static class OrderMapper
  {
    public static OrderDto ToDto(this Order model) => new OrderDto
    {
      TotalAmount = model.TotalAmount,
      Status = model.Status,
      Id = model.Id,
      CustomerId = model.CustomerId,
      CreatedAt = model.CreatedAt,
      UpdatedAt = model.UpdatedAt
    };

    public static IEnumerable<OrderDto> ToDto(this IEnumerable<Order> models) => models.Select(model => model.ToDto());

  }
}

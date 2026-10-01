using Domain.DataModel;
using Domain.Dto;

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
  }
}

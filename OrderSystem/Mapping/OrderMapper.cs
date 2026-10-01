using Domain.Dto;
using WebAPI.ViewModel;

namespace WebAPI.Mapping
{
  public static class OrderMapper
  {
    public static OrderViewModel ToViewModel(this OrderDto dto) => new OrderViewModel
    {
      CustomerId = dto.CustomerId,
      Id = dto.Id,
      Status = dto.Status,
      TotalAmount = dto.TotalAmount
    };
  }
}
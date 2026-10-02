using Domain.Dto;
using System.Collections.Generic;
using System.Linq;
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

    public static IEnumerable<OrderViewModel> ToViewModel(this IEnumerable<OrderDto> dtos) => dtos.Select(dto => dto.ToViewModel());
  }
}
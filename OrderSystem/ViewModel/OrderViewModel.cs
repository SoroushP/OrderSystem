using Domain.Enumeration;

namespace WebAPI.ViewModel
{
  public class OrderViewModel
  {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
  }
}
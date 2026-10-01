using Domain.Enumeration;
using System;

namespace Domain.DataModel
{
  public class Order
  {
    public int Id { get; set; }
    public int CustomerId {  get; set; }
    public decimal TotalAmount {  get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt {  get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}

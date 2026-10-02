using Domain.Enumeration;
using Newtonsoft.Json;
using Service.Security;
using WebAPI.JsonConverter;

namespace WebAPI.ViewModel
{
  public class OrderViewModel
  {
    [JsonConverter(typeof(EncryptedIdConverter))]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
  }
}
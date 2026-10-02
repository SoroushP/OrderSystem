using Domain.Enumeration;
using Newtonsoft.Json;
using Service.Security;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModel
{
  public class OrderViewModel
  {
    [JsonConverter(typeof(EncryptedIdConverter))]
    public int Id { get; set; }
    
    [Required]
    public int CustomerId { get; set; }
    
    [Range(0.0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Required]
    public OrderStatus Status { get; set; }
  }
}
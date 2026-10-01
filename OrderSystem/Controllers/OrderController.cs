using Domain.DataService;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Mapping;
using WebAPI.ViewModel;

namespace OrderSystem.Controllers
{
  public class OrderController : ApiController
  {
    private readonly IOrderService orderService;
    public OrderController(IOrderService orderService)
    {
      this.orderService = orderService;
    }

    // GET api/values/5
    public async Task<OrderViewModel> Get(int id)
    {
      var result = await orderService.Get(id);
      return result.ToViewModel();
    }
  }
}

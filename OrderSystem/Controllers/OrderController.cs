using Domain.DataService;
using Domain.Dto;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPI.Mapping;

namespace OrderSystem.Controllers
{
  public class OrderController : ApiController
  {
    private readonly IOrderService orderService;
    public OrderController(IOrderService orderService)
    {
      this.orderService = orderService;
    }

    [HttpGet]
    public async Task<IHttpActionResult> Get()
    {
      CancellationToken cancellationToken = HttpContext.Current.Response.ClientDisconnectedToken;
      var order = await orderService.Get(cancellationToken);

      if (order == null)
        return NotFound();

      return Json(order.ToViewModel());
    }

    [HttpGet]
    public async Task<IHttpActionResult> Get(int id)
    {
      CancellationToken cancellationToken = HttpContext.Current.Response.ClientDisconnectedToken;
      var order = await orderService.Get(id, cancellationToken);

      if (order == null)
        return NotFound();

      return Json(order.ToViewModel());
    }

    [HttpPost]
    public async Task<IHttpActionResult> Create(OrderDto dto)
    {
      if (dto == null)
      {
        return BadRequest("Request body is required.");
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      CancellationToken cancellationToken = HttpContext.Current.Response.ClientDisconnectedToken;
      var id = await orderService.InsertAsync(dto, cancellationToken);

      return Ok(id);
    }

    [HttpPut]
    public async Task<IHttpActionResult> Update(OrderDto dto)
    {
      if (dto == null)
      {
        return BadRequest("Request body is required.");
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      CancellationToken cancellationToken = HttpContext.Current.Response.ClientDisconnectedToken;
      var updated = await orderService.UpdateAsync(dto, cancellationToken);
      if (!updated)
        return NotFound();

      return Ok();
    }

    [HttpDelete]
    public async Task<IHttpActionResult> Delete(int id)
    {
      CancellationToken cancellationToken = HttpContext.Current.Response.ClientDisconnectedToken;
      var deleted = await orderService.DeleteAsync(id, cancellationToken);
      if (!deleted)
        return NotFound();

      return Ok();
    }
  }
}

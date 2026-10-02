using Domain.DataService;
using Domain.Dto;
using System.Threading.Tasks;
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
      var order = await orderService.Get();

      if (order == null)
        return NotFound();

      return Json(order.ToViewModel());
    }

    [HttpGet]
    public async Task<IHttpActionResult> Get(int id)
    {
      var order = await orderService.Get(id);

      if (order == null)
        return NotFound();

      return Json(order.ToViewModel());
    }

    [HttpPost]
    public async Task<IHttpActionResult> Create(OrderDto dto)
    {
      if (dto == null)
        return BadRequest("Request body is required.");

      var id = await orderService.InsertAsync(dto);

      return Ok(id);
    }

    [HttpPut]
    public async Task<IHttpActionResult> Update(OrderDto dto)
    {
      if (dto == null)
        return BadRequest("Request body is required.");

      var updated = await orderService.UpdateAsync(dto);
      if (!updated)
        return NotFound();

      return Ok();
    }

    [HttpDelete]
    public async Task<IHttpActionResult> Delete(int id)
    {
      var deleted = await orderService.DeleteAsync(id);
      if (!deleted)
        return NotFound();

      return Ok();
    }
  }
}

using Microsoft.AspNetCore.Mvc;
using Orders.Service.DataServices.Contracts;
using Orders.Service.ViewModels;

namespace Orders.Service.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        this.ordersService = ordersService;
    }

    [HttpPost]
    [Route("create/{userId:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderCreateResultViewModel))]
    public async Task<IActionResult> Create(
        [FromRoute] Guid userId,
        [FromBody] OrderCreateViewModel orderDetails)
    {
        OrderCreateResultViewModel order = await this.ordersService.Create(
            userId.ToString(),
            orderDetails);

        return this.Ok(order);
    }
}

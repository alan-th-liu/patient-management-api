using Microsoft.AspNetCore.Mvc;
using patient_management_api.Models.Api;
using patient_management_api.Services;

namespace patient_management_api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{patient_id}")]
    [ProducesResponseType(typeof(OrderApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderByPatientId([FromRoute(Name = "patient_id")] Guid patientId)
    {
        var response = await _orderService.GetOrdersAsync(patientId);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(OrderApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ModifyOrder([FromBody] OrderApiRequest request)
    {
        var isSuccess = await _orderService.UpdateAsync(request);
        if (!isSuccess)
            return StatusCode(500, "Internal server error");

        return Ok(isSuccess);
    }
}
using Footbally.Application.DTOs.Subscription;
using Footbally.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbally.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("admin/list")]
    public async Task<ActionResult<List<SubscriptionResponseDto>>> GetAdminList()
    {
        var subs = await _subscriptionService.GetAllAsync();
        return Ok(subs);
    }

    [HttpPut("{id}/extend")]
    public async Task<ActionResult<SubscriptionResponseDto>> Extend(int id)
    {
        var sub = await _subscriptionService.ExtendAsync(id);
        if (sub == null) return NotFound(new { message = "Abonelik bulunamadı." });
        return Ok(sub);
    }

    [HttpPut("{id}/cancel")]
    public async Task<ActionResult<SubscriptionResponseDto>> Cancel(int id)
    {
        var sub = await _subscriptionService.CancelAsync(id);
        if (sub == null) return NotFound(new { message = "Abonelik bulunamadı." });
        return Ok(sub);
    }

    [HttpPost("admin/grant")]
    public async Task<ActionResult<SubscriptionResponseDto>> Grant([FromBody] GrantSubscriptionDto dto)
    {
        try
        {
            var sub = await _subscriptionService.GrantAsync(dto);
            return Ok(sub);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

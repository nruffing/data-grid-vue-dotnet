using DataGridVueDotnet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DataGridVueDotnetExample.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class GridStateController : ControllerBase
  {
    private readonly IMemoryCache _cache;
    public GridStateController(IMemoryCache cache)
    {
      _cache = cache;
    }

    /// <summary>
    /// Gets current grid state for user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType<GridState>(200)]
    [ProducesResponseType(204)]
    public ActionResult<GridState> Get(GetGridStateRequest<string> request)
    {
      if (string.IsNullOrWhiteSpace(request?.UserId))
      {
        return BadRequest();
      }

      if (_cache.TryGetValue<GridState>(request.UserId, out var state))
      {
        return Ok(state);
      }

      return NoContent();
    }

    /// <summary>
    /// Sets current grid state for user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    public ActionResult Set(SetGridStateRequest<string> request)
    {
      if (string.IsNullOrWhiteSpace(request?.UserId))
      {
        return BadRequest();
      }

      if (request.GridState is null)
      {
        _cache.Remove(request.UserId);
      }

      _cache.Set(request.UserId, request.GridState, DateTimeOffset.Now.AddHours(1));

      return NoContent();
    }
  }
}

using DataGridVueDotnet;
using DataGridVueDotnet.Extensions;
using DataGridVueDotnetExample.Data;
using DataGridVueDotnetExample.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataGridVueDotnetExample.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class GridDataController : ControllerBase
  {
    private readonly TestContext _context;

    public GridDataController(TestContext context)
    {
      _context = context;
      _context.Database.Migrate();

      if (!_context.TestDataItems.Any())
      {
        _context.AddRange(MOCK_DATA.Data);
        _context.SaveChanges();
      }
    }

    /// <summary>
    /// Get all data items.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<TestDataItem[]>(200)]
    public async Task<ActionResult<TestDataItem[]>> GetAll()
    {
      return Ok(await _context.TestDataItems.ToArrayAsync());
    }

    /// <summary>
    /// Get page data based on request.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType<PageData<TestDataItem>>(200)]
    public async Task<ActionResult<PageData<TestDataItem>>> GetPageData(PageDataRequest request)
    {
      if (request is null || !request.IsValid)
      {
        return BadRequest();
      }

      var query = _context.TestDataItems.AsQueryable();
      var dataItems = await query
          .ApplyPageDataRequest(request)
          .ToArrayAsync();
      var count = await query
          .Filter(request)
          .CountAsync();

      return Ok(new PageData<TestDataItem>()
      {
        DataItems = dataItems,
        TotalItems = count
      });
    }
  }
}

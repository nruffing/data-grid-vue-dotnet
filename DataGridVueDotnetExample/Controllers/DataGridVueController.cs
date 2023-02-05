using DataGridVueDotnet;
using DataGridVueDotnet.Extensions;
using DataGridVueDotnetExample.Data;
using DataGridVueDotnetExample.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataGridVueDotnetExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataGridVueController : ControllerBase
    {
        private readonly TestContext _context;

        public DataGridVueController(TestContext context)
        {
            _context = context;
            _context.Database.Migrate();

            if (!_context.TestDataItems.Any()) 
            {
                _context.AddRange(MOCK_DATA.Data);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<TestDataItem[]>> Get()
        {
            return Ok(await _context.TestDataItems.ToArrayAsync());
        }

        [HttpPost]
        public async Task<ActionResult<PageData<TestDataItem>>> Post(PageDataRequest request) 
        {
            if (request is null || !request.IsValid)
            {
                return BadRequest();
            }

            var query = _context.TestDataItems.AsQueryable();
            var dataItems = await query
                .ApplyPageDataRequest(request)
                .ToArrayAsync();
            var count = await query.CountAsync();

            return Ok(new PageData<TestDataItem>()
            {
                DataItems = dataItems,
                TotalItems = count
            });
        }
    }
}
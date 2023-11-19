# data-grid-vue-dotnet
dotnet models and IQueryable extensions for handling server-side paging, sorting, and filtering for [data-grid-vue](https://github.com/nruffing/data-grid-vue)

## Example
```c#
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
    var count = await query
		.Filter(request)
		.CountAsync();

    return Ok(new PageData<TestDataItem>()
    {
        DataItems = dataItems,
        TotalItems = count
    });
}
```
A full example with an ASP.NET API and EF Core context can be seen in the _DataGridVueDotnetExample_ folder.
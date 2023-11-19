<p align="center">
  <img src="https://datagridvue.com/favicon.svg" width="100" style="margin: 15px 0;" />
</p>

<h1 align="center">Data Grid Vue dotnet</h1>

<p align="center">
  <a href="https://github.com/sponsors/nruffing">
    <img alt="GitHub Sponsors" src="https://img.shields.io/github/sponsors/nruffing?logo=github&color=%23ffa600">
  </a>
</p>
dotnet models and IQueryable extensions for handling server-side paging, sorting, filtering, and saving grid state for [data-grid-vue](https://datagridvue.com)

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

## Release Notes

### v1.0.0
 - Add models for server-side storage
 - Add documentation comments
 - Update readme

### v0.0.1-alpha
 - initial release

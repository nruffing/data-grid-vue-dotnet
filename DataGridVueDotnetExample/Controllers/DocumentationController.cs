using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using XmlDocMarkdown.Core;

namespace DataGridVueDotnetExample.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  [ApiExplorerSettings(IgnoreApi = true)]
  public class DocumentationController : ControllerBase
  {
    private const string XmlCacheKey = "DataGridVueDotnet-Docs-XML";
    private const string MarkdownCacheKey = "DataGridVueDotnet-Docs-Markdown";
    private const string XmlFileName = "DataGridVueDotnet.xml";
    private static readonly string XmlFilePath = Path.Combine(AppContext.BaseDirectory, XmlFileName);
    private static readonly string DllFilePath = Path.Combine(AppContext.BaseDirectory, "DataGridVueDotnet.dll");
    private static readonly string MarkdownDocsDirectory = Path.Combine(AppContext.BaseDirectory, "md-docs");

    private readonly IMemoryCache _cache;

    public DocumentationController(IMemoryCache cache)
    {
      _cache = cache;
    }

    [HttpGet]
    public async Task<ActionResult> GetXml()
    {
      if (!_cache.TryGetValue<byte[]>(XmlCacheKey, out var bytes))
      {
        var xml = await System.IO.File.ReadAllTextAsync(XmlFilePath);
        bytes = Encoding.UTF8.GetBytes(xml);
        _cache.Set(XmlCacheKey, bytes);
      }

      if (bytes is null)
      {
        return NoContent();
      }

      return File(bytes, "application/xml; charset=utf-8", XmlFileName);
    }

    [HttpGet]
    public ActionResult GetMarkdown()
    {
      if (!_cache.TryGetValue<byte[]>(MarkdownCacheKey, out var bytes))
      {
        var settings = new XmlDocMarkdownSettings()
        {
          SourceCodePath = "https://github.com/nruffing/data-grid-vue-dotnet/tree/main/DataGridVueDotnet",
          RootNamespace = "DataGridVueDotnet",
          ShouldClean = true,
        };
        var result = XmlDocMarkdownGenerator.Generate(DllFilePath, MarkdownDocsDirectory, settings);

        using var stream = new MemoryStream();
        ZipFile.CreateFromDirectory(MarkdownDocsDirectory, stream, CompressionLevel.Fastest, false);
        bytes = stream.ToArray();
        _cache.Set(MarkdownCacheKey, bytes);
      }

      if (bytes is null)
      {
        return NoContent();
      }

      return File(bytes, "application/xml; charset=utf-8", $"{MarkdownCacheKey}.zip");
    }
  }
}

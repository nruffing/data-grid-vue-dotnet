using DataGridVueDotnet.Exceptions;

namespace DataGridVueDotnet
{
  /// <summary>
  /// Request data interface sent by the data grid's
  /// <see href="https://datagridviue.com/generated/classes/ServerSideDataService.html">Server Side Data Service</see>
  /// </summary>
  public class PageDataRequest
  {
    /// <summary>
    /// The page number for the page to load starting with 1 for the first page.
    /// If the data grid is not set configured to be pageable then this will always be -1.
    /// </summary>
    public int PageNum { get; set; }

    /// <summary>
    /// The maximum number of data items to display on each page. If the data grid is
    /// not set configured to be pageable then this will always be `-1`.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The current column sort definitions in the order in which they should be applied.
    /// </summary>
    public Sort[] Sort { get; set; } = Array.Empty<Sort>();

    /// <summary>
    /// The current filter definition or null if no filter is set.
    /// </summary>
    public Filter? Filter { get; set; }

    /// <summary>
    /// True if the request is valid, false otherwise.
    /// </summary>
    public bool IsValid =>
        PageNum > 0 &&
        PageSize > 0 &&
        Sort.All(s => s.IsValid) &&
        (Filter is null || Filter.IsValid);
  }

  /// <summary>
  /// Extension methods for <see cref="PageDataRequest"/>
  /// </summary>
  public static class PageDataRequestExtensions
  {
    /// <summary>
    /// Throws a <see cref="PageDataRequestInvalidException"/> if the request is not valid.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    /// <exception cref="PageDataRequestInvalidException">Thrown if the request is not valid.</exception>
    public static void Validate(this PageDataRequest? request)
    {
      if (request is null || !request.IsValid)
      {
        throw new PageDataRequestInvalidException(request);
      }
    }
  }
}

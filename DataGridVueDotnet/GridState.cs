namespace DataGridVueDotnet
{
  /// <summary>
  /// The data that is saved as part of the grid state.
  /// <see href="https://datagridvue.com/generated/classes/ServerSideStorageService.html">Server Side Storage Service</see>
  /// </summary>
  public class GridState
  {
    /// <summary>
    /// The current page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The field names of the hidden columns
    /// </summary>
    public string[] HiddenFields { get; set; } = Array.Empty<string>();

    /// <summary>
    /// The current sort definition
    /// </summary>
    public Sort[] Sort { get; set; } = Array.Empty<Sort>();

    /// <summary>
    /// The current filter conditions
    /// </summary>
    public FilterCondition[] Filters { get; set; } = Array.Empty<FilterCondition>();

    /// <summary>
    /// The current external filter if applied
    /// </summary>
    public Filter? ExternalFilter { get; set; }

    /// <summary>
    /// The field name of the columns in the order they are currently displayed
    /// </summary>
    public string[] ColumnOrder { get; set; } = Array.Empty<string>();
  }
}

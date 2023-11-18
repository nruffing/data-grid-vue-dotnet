namespace DataGridVueDotnet
{
  /// <summary>
  /// Model definition for the data displaying on the current page.
  /// <seealso href="https://datagridvue.com/generated/interfaces/PageData.html"/>
  /// </summary>
  /// <typeparam name="TDataItem">The type of each data item.</typeparam>
  public class PageData<TDataItem>
  {
    /// <summary>
    /// The total number of data items after current filters are applied across all pages.
    /// </summary>
    public long TotalItems { get; set; }

    /// <summary>
    /// The data items for the current page.
    /// </summary>
    public TDataItem[] DataItems { get; set; } = Array.Empty<TDataItem>();
  }
}

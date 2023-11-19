namespace DataGridVueDotnet
{
  /// <summary>
  /// Model definition for the aggregated filter currently being applied to the entire data grid.
  /// <seealso href="https://datagridvuew.com/generated/interfaces/Filter.html"/>
  /// </summary>
  public class Filter
  {
    /// <summary>
    /// Collection of <see cref="FilterCondition"/>s that will be or-ed together.
    /// </summary>
    public FilterCondition[] Or { get; set; } = Array.Empty<FilterCondition>();

    /// <summary>
    /// Optional <see cref="Filter"/> to and with the current one.
    /// </summary>
    public Filter? And { get; set; }

    /// <summary>
    /// True if the filter is valid, false otherwise.
    /// </summary>
    public bool IsValid =>
        Or != null &&
        Or.Any() &&
        Or.All(c => c.IsValid) &&
        (And is null || And.IsValid);

    public override string ToString()
    {
      var s = $"{string.Join(" OR ", Or.Select(c => c.ToString()))}";
      if (And != null)
      {
        s = $"({s}) AND {And}";
      }
      return s;
    }
  }
}

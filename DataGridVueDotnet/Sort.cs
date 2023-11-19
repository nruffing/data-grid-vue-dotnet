namespace DataGridVueDotnet
{
  /// <summary>
  /// Column sort definition.
  /// <seealso href="https://datagridvue.com/generated/interfaces/Sort.html"/>
  /// </summary>
  public class Sort
  {
    /// <summary>
    /// The field name that the data is being sorted by.
    /// </summary>
    public string FieldName { get; set; } = "";

    /// <summary>
    /// The <see cref="DataType"/> for the column being sorted.
    /// </summary>
    public DataType DataType { get; set; }

    /// <summary>
    /// The <see cref="SortType"/> (e.g. ascending or descending).
    /// </summary>
    public SortType Type { get; set; }

    /// <summary>
    /// True if the sort is valid, false otherwise.
    /// </summary>
    public bool IsValid => !string.IsNullOrEmpty(FieldName) && DataType != DataType.None;

    public override string ToString()
    {
      var type = Type == SortType.Ascending ? "ascending" : "descending";
      return $"{FieldName} {type}";
    }
  }
}

namespace DataGridVueDotnet
{
  /// <summary>
  /// Model definition for the current state of a column filter.
  /// <seealso href="https://datagridvue.com/generated/interfaces/FilterCondition.html"/>
  /// </summary>
  public class FilterCondition
  {
    /// <summary>
    /// The name of the field being filtered by.
    /// </summary>
    public string FieldName { get; set; } = "";

    /// <summary>
    /// The <see cref="FilterOperator"/> being applied.
    /// </summary>
    public FilterOperator Operator { get; set; }

    /// <summary>
    /// The <see cref="DataType"/> of the column being filtered.
    /// </summary>
    public DataType DataType { get; set; }

    /// <summary>
    /// The current filter value.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// True if the condition is valid, false otherwise.
    /// </summary>
    public bool IsValid => !string.IsNullOrEmpty(FieldName) && DataType.IsValidFilterOperator(Operator);

    public override string ToString()
    {
      return $"{FieldName} ({DataType}) {Operator} {Value}";
    }
  }
}

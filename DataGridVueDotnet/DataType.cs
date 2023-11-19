using System.Collections.ObjectModel;

namespace DataGridVueDotnet
{
  /// <summary>
  /// Supported data types for a data grid column.
  /// <seealso href="https://datagridvue.com/generated/enumerations/DataType.html"/>
  /// </summary>
  public enum DataType
  {
    None = 0,
    Alphanumeric = 1,
    Number = 2,
    Date = 3,
    DateTime = 4,
  }

  /// <summary>
  /// Extension methods for <see cref="DataType"/>
  /// </summary>
  public static class DataTypeExtensions
  {
    /// <summary>
    /// Map of <see cref="DataType"/> to a collection of valid <see cref="FilterOperator"/>.
    /// <seealso href="https://datagridvue.com/generated/variables/ValidOperatorsMap.html"/>
    /// <seealso href="https://datagridvue.com/generated/enumerations/DataType.html"/>
    /// <seealso href="https://datagridvue.com/generated/enumerations/FilterOperator.html"/>
    /// </summary>
    public static readonly ReadOnlyDictionary<DataType, FilterOperator[]> ValidFilterOperatorMap = new ReadOnlyDictionary<DataType, FilterOperator[]>(
        new Dictionary<DataType, FilterOperator[]>()
        {
                { DataType.None, Array.Empty<FilterOperator>() },
                {
                    DataType.Alphanumeric,
                    new []
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.Contains,
                        FilterOperator.StartsWith,
                        FilterOperator.EndsWith,
                    }
                },
                {
                    DataType.Number,
                    new []
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.GreaterThan,
                        FilterOperator.LessThan,
                        FilterOperator.GreaterThanOrEqualTo,
                        FilterOperator.LessThanOrEqualTo,
                    }
                },
                {
                    DataType.Date,
                    new []
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.GreaterThan,
                        FilterOperator.LessThan,
                        FilterOperator.GreaterThanOrEqualTo,
                        FilterOperator.LessThanOrEqualTo,
                    }
                },
                {
                    DataType.DateTime,
                    new []
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.GreaterThan,
                        FilterOperator.LessThan,
                        FilterOperator.GreaterThanOrEqualTo,
                        FilterOperator.LessThanOrEqualTo,
                    }
                },
        }
    );

    /// <summary>
    /// Returns whether the <see cref="FilterOperator"/> is valid for the <see cref="DataType"/>.
    /// <seealso href="https://datagridvue.com/generated/variables/ValidOperatorsMap.html"/>
    /// <seealso href="https://datagridvue.com/generated/enumerations/DataType.html"/>
    /// <seealso href="https://datagridvue.com/generated/enumerations/FilterOperator.html"/>
    /// </summary>
    /// <param name="dataType">The <see cref="DataType"/></param>
    /// <param name="filterOperator">The <see cref="FilterOperator"/></param>
    /// <returns>True if the <see cref="FilterOperator"/> is valid, false otherwise,</returns>
    public static bool IsValidFilterOperator(this DataType dataType, FilterOperator filterOperator)
    {
      if (!ValidFilterOperatorMap.TryGetValue(dataType, out var validOperators))
      {
        throw new InvalidOperationException($"Valid filter operator map is not implemented for data type {dataType}");
      }
      return validOperators.Contains(filterOperator);
    }
  }
}

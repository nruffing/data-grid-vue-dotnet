using System.Linq.Expressions;

namespace DataGridVueDotnet
{
  /// <summary>
  /// Supported filter operators.
  /// <seealso href="https://datagridvue.com/generated/enumerations/FilterOperator.html"/>
  /// </summary>
  public enum FilterOperator
  {
    Equals = 0,
    NotEquals = 1,
    Contains = 2,
    StartsWith = 3,
    EndsWith = 4,
    GreaterThan = 5,
    LessThan = 6,
    GreaterThanOrEqualTo = 7,
    LessThanOrEqualTo = 8,
  }

  /// <summary>
  /// Extension methods for <see cref="FilterOperator"/>.
  /// </summary>
  public static class FilterOperatorExtensions
  {
    /// <summary>
    /// Builds an <see cref="Expression"/> for the <see cref="FilterOperator"/> using the supplied <see cref="MemberExpression"/>
    /// and <see cref="ConstantExpression"/> as the operands.
    /// </summary>
    /// <param name="filterOperator">The <see cref="FilterOperator"/></param>
    /// <param name="propertyAccess">The <see cref="MemberExpression"/> that accesses the property mapped to the filter condition.</param>
    /// <param name="value">The <see cref="ConstantExpression"/> with the filter value.</param>
    /// <returns>The built <see cref="Expression"/></returns>
    public static Expression GetExpression(this FilterOperator filterOperator, MemberExpression propertyAccess, ConstantExpression value)
    {
      switch (filterOperator)
      {
        case FilterOperator.Equals:
          return Expression.Equal(propertyAccess, value);
        case FilterOperator.NotEquals:
          return Expression.NotEqual(propertyAccess, value);
        case FilterOperator.Contains:
          return Expression.Call(propertyAccess, "Contains", Array.Empty<Type>(), value);
        case FilterOperator.StartsWith:
          return Expression.Call(propertyAccess, "StartsWith", Array.Empty<Type>(), value);
        case FilterOperator.EndsWith:
          return Expression.Call(propertyAccess, "EndsWith", Array.Empty<Type>(), value);
        case FilterOperator.GreaterThan:
          return Expression.GreaterThan(propertyAccess, value);
        case FilterOperator.LessThan:
          return Expression.LessThan(propertyAccess, value);
        case FilterOperator.GreaterThanOrEqualTo:
          return Expression.GreaterThanOrEqual(propertyAccess, value);
        case FilterOperator.LessThanOrEqualTo:
          return Expression.LessThanOrEqual(propertyAccess, value);
      }

      throw new NotImplementedException($"Expression not defined for filter operator {filterOperator}");
    }
  }
}

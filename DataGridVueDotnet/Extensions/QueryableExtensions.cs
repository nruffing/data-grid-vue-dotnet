using DataGridVueDotnet.Internal;
using System.Linq.Expressions;

namespace DataGridVueDotnet.Extensions
{
  /// <summary>
  /// Extension methods for <see cref="IQueryable"/>
  /// </summary>
  public static class QueryableExtensions
  {
    /// <summary>
    /// Applies a <see cref="PageDataRequest"/> to the <see cref="IQueryable"/>.
    /// </summary>
    /// <typeparam name="TDataItem">The type of the data items being queried.</typeparam>
    /// <param name="query">The <see cref="IQueryable"/></param>
    /// <param name="request">The <see cref="PageDataRequest"/></param>
    /// <returns>The updated <see cref="IQueryable"/>.</returns>
    public static IQueryable<TDataItem> ApplyPageDataRequest<TDataItem>(this IQueryable<TDataItem> query, PageDataRequest request)
    {
      request.Validate();

      return query
          .Filter(request)
          .Sort(request)
          .Page(request);
    }

    #region Page

    /// <summary>
    /// Adds paging expressions to the <see cref="IQueryable"/>.
    /// </summary>
    /// <typeparam name="TDataItem">The type of the data items being queried.</typeparam>
    /// <param name="query">The <see cref="IQueryable"/></param>
    /// <param name="request">The <see cref="PageDataRequest"/></param>
    /// <returns>The updated <see cref="IQueryable"/>.</returns>
    public static IQueryable<TDataItem> Page<TDataItem>(this IQueryable<TDataItem> query, PageDataRequest request)
    {
      request.Validate();

      return query
          .Skip((request.PageNum - 1) * request.PageSize)
          .Take(request.PageSize);
    }

    #endregion

    #region Sort

    /// <summary>
    /// Adds sort expressions to the <see cref="IQueryable"/>.
    /// </summary>
    /// <typeparam name="TDataItem">The type of the data items being queried.</typeparam>
    /// <param name="query">The <see cref="IQueryable"/></param>
    /// <param name="request">The <see cref="PageDataRequest"/></param>
    /// <returns>The updated <see cref="IQueryable"/>.</returns>
    public static IQueryable<TDataItem> Sort<TDataItem>(this IQueryable<TDataItem> query, PageDataRequest request)
    {
      request.Validate();

      if (!(request.Sort?.Any() ?? false))
      {
        return query;
      }

      var firstSort = request.Sort.First();
      var ordered = query.Sort(firstSort, true);

      foreach (var sort in request.Sort.Skip(1))
      {
        ordered = ordered.Sort(sort, false);
      }

      return ordered;
    }

    private static IOrderedQueryable<TDataItem> Sort<TDataItem>(this IQueryable<TDataItem> query, Sort sort, bool isFirst)
    {
      var propertyAccess = PropertyAccessorFactory.Create<TDataItem>(sort.FieldName);
      return propertyAccess.Sort(query, sort.Type, isFirst);
    }

    #endregion

    #region Filter

    /// <summary>
    /// Adds filter expressions to the <see cref="IQueryable"/>
    /// </summary>
    /// <typeparam name="TDataItem">The type of the data items being queried.</typeparam>
    /// <param name="query">The <see cref="IQueryable"/></param>
    /// <param name="request">The <see cref="PageDataRequest"/></param>
    /// <returns>The updated <see cref="IQueryable"/>.</returns>
    public static IQueryable<TDataItem> Filter<TDataItem>(this IQueryable<TDataItem> query, PageDataRequest request)
    {
      if (request.Filter is null)
      {
        return query;
      }

      return query.Filter(request.Filter);
    }

    private static IQueryable<TDataItem> Filter<TDataItem>(this IQueryable<TDataItem> query, Filter filter)
    {
      var dataItemParameter = Expression.Parameter(typeof(TDataItem));

      var conditionExpression = GetFilterConditionExpression<TDataItem>(dataItemParameter, filter.Or.First());
      foreach (var condition in filter.Or.Skip(1))
      {
        conditionExpression = Expression.Or(conditionExpression, GetFilterConditionExpression<TDataItem>(dataItemParameter, condition));
      }

      var lambdaExpression = Expression.Lambda<Func<TDataItem, bool>>(conditionExpression, dataItemParameter);
      query = query.Where(lambdaExpression);

      if (filter.And is null)
      {
        return query;
      }
      else
      {
        return query.Filter(filter.And);
      }
    }

    private static Expression GetFilterConditionExpression<TDataItem>(ParameterExpression parameter, FilterCondition condition)
    {
      var propertyAccess = PropertyAccessorFactory.Create<TDataItem>(condition.FieldName);
      return propertyAccess.GetFilterConditionExpression(parameter, condition.Operator, condition.Value);
    }

    #endregion
  }
}

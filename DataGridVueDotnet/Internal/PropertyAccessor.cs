using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace DataGridVueDotnet.Internal
{
    internal interface IPropertyAccessor<TDataItem>
    {
        IOrderedQueryable<TDataItem> Sort(IQueryable<TDataItem> query, SortType sortType, bool isFirst);
        Expression GetFilterConditionExpression(ParameterExpression parameter, FilterOperator filterOperator, string? value);
    }

    internal class PropertyAccessor<TDataItem, TProperty>: IPropertyAccessor<TDataItem>
    {
        private readonly string _fieldName;

        public PropertyAccessor(string fieldName)
        {
            _fieldName = fieldName;
        }

        public IOrderedQueryable<TDataItem> Sort(IQueryable<TDataItem> query, SortType sortType, bool isFirst)
        {
            var dataItemParameter = Expression.Parameter(typeof(TDataItem));
            var propertyAccess = Expression.Property(dataItemParameter, _fieldName);
            var propertyAccessLambda = Expression.Lambda<Func<TDataItem, TProperty>>(propertyAccess, dataItemParameter);

            if (isFirst)
            {
                return sortType == SortType.Ascending
                    ? query.OrderBy(propertyAccessLambda)
                    : query.OrderByDescending(propertyAccessLambda);
            }
            else
            {
                var ordered = query as IOrderedQueryable<TDataItem>;
                if (ordered is null)
                {
                    throw new InvalidOperationException("Secondary sort was executed prior to primary.");
                }

                return sortType == SortType.Ascending
                    ? ordered.ThenBy(propertyAccessLambda)
                    : ordered.ThenByDescending(propertyAccessLambda);
            }
        }

        public Expression GetFilterConditionExpression(ParameterExpression parameter, FilterOperator filterOperator, string? value)
        {
            var valueExpression = typeof(TProperty) == typeof(string)
                ? Expression.Constant(value)
                : Expression.Constant(value is null ? default : JsonSerializer.Deserialize<TProperty>($"{value}"));
            
            var propertyAccess = Expression.Property(parameter, _fieldName);
            return filterOperator.GetExpression(propertyAccess, valueExpression);
        }
    }

    internal static class PropertyAccessorFactory
    {
        private static readonly Type PropertyAccessType = Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name.StartsWith("PropertyAccessor"));

        public static IPropertyAccessor<TDataItem> Create<TDataItem>(string fieldName)
        {
            var dataItemType = typeof(TDataItem);
            var property = dataItemType.GetProperties().FirstOrDefault(p => p.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
            if (property is null)
            {
                throw new InvalidOperationException($"Could not find property {fieldName} on {dataItemType}");
            }

            var propertyAccessType = PropertyAccessType.MakeGenericType(typeof(TDataItem), property.PropertyType);
            var propertyAccess =  Activator.CreateInstance(propertyAccessType, fieldName) as IPropertyAccessor<TDataItem>;
            
            if (propertyAccess is null)
            {
                throw new Exception($"Failed to create property accessor for field {fieldName}.");
            }

            return propertyAccess;
        }
    }
}

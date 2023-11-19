using DataGridVueDotnet.Internal;
using System.Reflection;

namespace DataGridVueDotnet.Extensions
{
    public static class QueryableExtensions
    {
        private static readonly Type PropertyAccessType = Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name.StartsWith("PropertyAccessor"));
     
        public static IQueryable<TDataItem> ApplyPageDataRequest<TDataItem>(this IQueryable<TDataItem> query, PageDataRequest request)
        {
            request.Validate();

            return query
                .Sort(request)
                .Page(request);
        }

        public static IQueryable<TDataItem> Page<TDataItem>(this IQueryable<TDataItem> query, PageDataRequest request)
        {
            request.Validate();

            return query
                .Skip((request.PageNum - 1) * request.PageSize)
                .Take(request.PageSize);
        }

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

        public static IOrderedQueryable<TDataItem> Sort<TDataItem>(this IQueryable<TDataItem> query, Sort sort, bool isFirst)
        {
            var dataItemType = typeof(TDataItem);
            var property = dataItemType.GetProperties().FirstOrDefault(p => p.Name.Equals(sort.FieldName, StringComparison.OrdinalIgnoreCase));
            if (property is null)
            {
                throw new InvalidOperationException($"Could not find property {sort.FieldName} on {dataItemType}");
            }

            var propertyAccessType = PropertyAccessType.MakeGenericType(typeof(TDataItem), property.PropertyType);
            var propertyAccess = Activator.CreateInstance(propertyAccessType, sort.FieldName) as IPropertyAccessor<TDataItem>;
            
            if (propertyAccess is null)
            {
                throw new Exception($"Failed to create property accessor for sort {sort}.");
            }

            return propertyAccess.Sort(query, sort.Type, isFirst);
        }
    }
}

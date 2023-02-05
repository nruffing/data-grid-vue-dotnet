using DataGridVueDotnet.Internal;
using System.Reflection;

namespace DataGridVueDotnet.Extensions
{
    public static class QueryableExtensions
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        private static readonly Type PropertyAccessType = Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name.StartsWith("PropertyAccessor"));
        #pragma warning restore CS8601 // Possible null reference assignment.

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
            var propertyAccessType = PropertyAccessType.MakeGenericType(typeof(TDataItem), sort.DataType.GetAssociatedType());
            var propertyAccess = Activator.CreateInstance(propertyAccessType, sort.FieldName) as IPropertyAccessor<TDataItem>;
            
            if (propertyAccess is null)
            {
                throw new Exception($"Failed to create property accessor for sort {sort}.");
            }

            return propertyAccess.Sort(query, sort.Type, isFirst);
        }
    }
}

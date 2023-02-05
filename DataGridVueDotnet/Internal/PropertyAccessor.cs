using System.Linq.Expressions;

namespace DataGridVueDotnet.Internal
{
    internal interface IPropertyAccessor<TDataItem>
    {
        IOrderedQueryable<TDataItem> Sort(IQueryable<TDataItem> query, SortType sortType, bool isFirst);
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
            var propertyAccessLamda = Expression.Lambda<Func<TDataItem, TProperty>>(propertyAccess, dataItemParameter);
            return query.OrderBy(propertyAccessLamda);
        }
    }
}

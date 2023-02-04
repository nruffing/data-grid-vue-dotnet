namespace DataGridViewDotnet.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDataItem> Page<TDataItem>(this IQueryable<TDataItem> query, PageDataRequest request)
        {
            request.Validate();
            return query
                .Skip((request.PageNum - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        public static void Validate(this PageDataRequest? request)
        {
            if (request is null || !request.IsValid)
            {
                throw new InvalidOperationException("PageDataRequest is invalid");
            }
        }
    }
}

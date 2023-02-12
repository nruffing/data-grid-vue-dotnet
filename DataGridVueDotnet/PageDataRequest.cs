using DataGridVueDotnet.Exceptions;

namespace DataGridVueDotnet
{
    public class PageDataRequest
    {
        public int PageNum { get; set; }

        public int PageSize { get; set; }

        public Sort[] Sort { get; set; } = Array.Empty<Sort>();

        public Filter? Filter { get; set; }

        public bool IsValid => 
            PageNum > 0 && 
            PageSize > 0 &&
            Sort.All(s => s.IsValid) &&
            (Filter is null || Filter.IsValid);
    }

    public static class PageDataRequestExtensions
    {
        public static void Validate(this PageDataRequest? request)
        {
            if (request is null || !request.IsValid)
            {
                throw new PageDataRequestInvalidException(request);
            }
        }
    }
}

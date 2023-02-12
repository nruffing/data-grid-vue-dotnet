namespace DataGridVueDotnet.Exceptions
{
    public class PageDataRequestInvalidException : Exception
    {
        public PageDataRequest? Request { get; init; }

        public PageDataRequestInvalidException(PageDataRequest? request)
            : base("PageDataRequest is invalid")
        {
            Request = request;
        }
    }
}

namespace DataGridVueDotnet.Extensions
{
    public static class ValidationExtensions
    {
        public static void Validate(this PageDataRequest? request)
        {
            if (request is null || !request.IsValid)
            {
                throw new InvalidOperationException("PageDataRequest is invalid.");
            }
        }

        public static void Validate(this Sort? sort)
        {
            if (sort is null || !sort.IsValid)
            {
                throw new InvalidOperationException("PageDataRequest contains an invalid sort definition.");
            }
        }
    }
}

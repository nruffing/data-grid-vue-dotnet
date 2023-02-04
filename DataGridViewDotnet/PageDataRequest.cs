namespace DataGridViewDotnet
{
    public class PageDataRequest
    {
        public long PageNum { get; set; }

        public long PageSize { get; set; }

        public Sort[] Sort { get; set; } = Array.Empty<Sort>();

        public Filter? Filter { get; set; }
    }
}

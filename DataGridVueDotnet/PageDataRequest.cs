namespace DataGridVueDotnet
{
    public class PageDataRequest
    {
        public int PageNum { get; set; }

        public int PageSize { get; set; }

        public Sort[] Sort { get; set; } = Array.Empty<Sort>();

        public Filter? Filter { get; set; }

        public bool IsValid => PageNum > 0 && PageSize > 0;
    }
}

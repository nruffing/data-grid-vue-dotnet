namespace DataGridViewDotnet
{
    public class PageData<TDataItem>
    {
        public long TotalItems { get; set; }

        public TDataItem[] DataItems { get; set; } = Array.Empty<TDataItem>(); 
    }
}

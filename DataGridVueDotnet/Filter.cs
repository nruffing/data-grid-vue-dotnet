namespace DataGridVueDotnet
{
    public class Filter
    {
        public FilterCondition[] Or { get; set; } = Array.Empty<FilterCondition>();

        public Filter? And { get; set; }
    }
}

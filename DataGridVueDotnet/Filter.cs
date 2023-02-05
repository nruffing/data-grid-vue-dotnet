namespace DataGridVueDotnet
{
    public class Filter
    {
        public FilterCondition[] Or { get; set; } = Array.Empty<FilterCondition>();

        public Filter? And { get; set; }

        public override string ToString()
        {
            var s =  $"{string.Join(" OR ", Or.Select(x => x.ToString()))}";
            if (And != null)
            {
                s = $"({s}) AND {And}";
            }
            return s;
        }
    }
}

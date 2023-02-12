namespace DataGridVueDotnet
{
    public class Filter
    {
        public FilterCondition[] Or { get; set; } = Array.Empty<FilterCondition>();

        public Filter? And { get; set; }

        public bool IsValid => 
            Or != null && 
            Or.Any() &&
            Or.All(c => c.IsValid) &&
            (And is null || And.IsValid);

        public override string ToString()
        {
            var s =  $"{string.Join(" OR ", Or.Select(c => c.ToString()))}";
            if (And != null)
            {
                s = $"({s}) AND {And}";
            }
            return s;
        }
    }
}

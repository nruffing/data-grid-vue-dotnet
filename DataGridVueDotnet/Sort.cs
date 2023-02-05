namespace DataGridVueDotnet
{
    public class Sort
    {
        public string FieldName { get; set; } = "";

        public DataType DataType { get; set; }

        public SortType Type { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(FieldName) && DataType != DataType.None;

        public override string ToString()
        {
            var type = Type == SortType.Ascending ? "ascending" : "descending";
            return $"{FieldName} {type}";
        }
    }
}

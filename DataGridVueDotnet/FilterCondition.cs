namespace DataGridVueDotnet
{
    public class FilterCondition
    {
        public string FieldName { get; set; } = "";

        public FilterOperator Operator { get; set; }

        public DataType DataType { get; set; }

        public dynamic? Value { get; set; }

        public override string ToString()
        {
            return $"{FieldName} ({DataType}) {Operator} {Value}";
        }
    }
}

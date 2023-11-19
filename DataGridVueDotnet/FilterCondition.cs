using System.Linq.Expressions;

namespace DataGridVueDotnet
{
    public class FilterCondition
    {
        public string FieldName { get; set; } = "";

        public FilterOperator Operator { get; set; }

        public DataType DataType { get; set; }

        public string? Value { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(FieldName) && DataType.IsValidFilterOperator(Operator);

        public override string ToString()
        {
            return $"{FieldName} ({DataType}) {Operator} {Value}";
        }
    }
}

namespace DataGridVueDotnet.Exceptions
{
    public class DataParseFailedException : Exception
    {
        public DataType DataType { get; init; }
        public string? Value { get; init; }

        public DataParseFailedException(DataType dataType, string? value)
            : base($"Could not parse value {value} to data type {dataType}")
        {
            DataType = dataType;
            Value = value;
        }
    }
}

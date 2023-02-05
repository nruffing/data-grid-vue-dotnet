namespace DataGridVueDotnet.Extensions
{
    public static class DataTypeExtensions
    {
        public static Type GetAssociatedType(this DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Alphanumeric:
                    return typeof(string);
                case DataType.Number:
                    return typeof(long);
                case DataType.Date:
                    return typeof(DateOnly);
                case DataType.DateTime:
                    return typeof(DateTimeOffset);
            }

            throw new NotImplementedException($"Associated type is not known for {dataType} data type.");
        }
    }
}

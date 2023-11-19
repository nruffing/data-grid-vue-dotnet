using System.Collections.ObjectModel;

namespace DataGridVueDotnet
{
    public enum DataType
    {
        None = 0,
        Alphanumeric = 1,
        Number = 2,
        Date = 3,
        DateTime = 4,
    }

    public static class DataTypeExtensions
    {
        public static readonly ReadOnlyDictionary<DataType, FilterOperator[]> ValidFilterOperatorMap = new ReadOnlyDictionary<DataType, FilterOperator[]>(
            new Dictionary<DataType, FilterOperator[]>()
            {
                { DataType.None, Array.Empty<FilterOperator>() },
                { 
                    DataType.Alphanumeric,
                    new [] 
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.Contains,
                        FilterOperator.StartsWith,
                        FilterOperator.EndsWith,
                    }
                },
                {
                    DataType.Number,
                    new []
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.GreaterThan,
                        FilterOperator.LessThan,
                        FilterOperator.GreaterThanOrEqualTo,
                        FilterOperator.LessThanOrEqualTo,
                    }
                },
                {
                    DataType.Date,
                    new []
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.GreaterThan,
                        FilterOperator.LessThan,
                        FilterOperator.GreaterThanOrEqualTo,
                        FilterOperator.LessThanOrEqualTo,
                    }
                },
                {
                    DataType.DateTime,
                    new []
                    {
                        FilterOperator.Equals,
                        FilterOperator.NotEquals,
                        FilterOperator.GreaterThan,
                        FilterOperator.LessThan,
                        FilterOperator.GreaterThanOrEqualTo,
                        FilterOperator.LessThanOrEqualTo,
                    }
                },
            }
        );

        public static bool IsValidFilterOperator(this DataType dataType, FilterOperator filterOperator)
        {
            if (!ValidFilterOperatorMap.TryGetValue(dataType, out var validOperators))
            {
                throw new InvalidOperationException($"Valid filter operator map is not implemented for data type {dataType}");
            }
            return validOperators.Contains(filterOperator);
        }
    }
}

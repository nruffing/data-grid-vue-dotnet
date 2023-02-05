namespace DataGridVueDotnetExample.Data.Models
{
    public class TestDataItem
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public long? PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}

using DataGridVueDotnetExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataGridVueDotnetExample.Data
{
    public class TestContext : DbContext
    {
        public DbSet<TestDataItem> TestDataItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=file::memory:?cache=shared");
        }
    }
}

using DataGridVueDotnetExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataGridVueDotnetExample.Data
{
  public class TestContext : DbContext
  {
    public DbSet<TestDataItem> TestDataItems { get; set; }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options) { }
  }
}

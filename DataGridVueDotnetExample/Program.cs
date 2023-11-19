using DataGridVueDotnetExample.Data;
using Microsoft.EntityFrameworkCore;

namespace DataGridVueDotnetExample
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Logging.ClearProviders();
      builder.Logging.AddConsole();

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddDbContext<TestContext>(optionsBuilder =>
      {
        optionsBuilder
                  .UseSqlite("DataSource=file::memory:?cache=shared");
      });

      builder.Services.AddCors(options =>
      {
        options.AddPolicy(
                  "AllowLocal",
                  policy =>
                  {
                policy
                          .WithOrigins("http://localhost:5173", "http://localhost:8080", "https://datagridvue.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
              }
              );
      });

      var app = builder.Build();

      app.UseCors("AllowLocal");

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}

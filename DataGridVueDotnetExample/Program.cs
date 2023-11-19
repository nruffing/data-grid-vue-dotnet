using System.Reflection;
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
      builder.Services.AddMemoryCache();

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
          Version = "v1",
          Title = "Data Grid Vue Example API",
          Description = "Example API demonstrating how to implement server-side paging, sorting, filtering and grid state storage when using data-grid-vue and data-grid-vue-dotnet.",
          Contact = new Microsoft.OpenApi.Models.OpenApiContact()
          {
            Name = "Nicholas Ruffing",
            Email = "nicholasruffing70@gmail.com",
          },
          License = new Microsoft.OpenApi.Models.OpenApiLicense()
          {
            Name = "MIT",
            Url = new Uri("https://github.com/nruffing/data-grid-vue-dotnet/blob/main/LICENSE"),
          }
        });
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DataGridVueDotnet.xml"));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml"), true);
      });

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

      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.DocumentTitle = "Swagger - Data Grid Vue Example API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "";
        options.InjectStylesheet("swagger-data-grid-vue.css");
        options.ConfigObject.DefaultModelRendering = Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model;
        options.HeadContent = "test";
      });

      app.UseHttpsRedirection();
      app.UseAuthorization();
      app.MapControllers();
      app.UseStaticFiles();

      app.Run();
    }
  }
}

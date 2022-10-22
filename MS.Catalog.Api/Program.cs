using Core.Common.Cqrs;
using Core.Common.Cqrs.Commands;
using Core.Infrastructure.GuidGenerator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MS.Catalog.Application.Products.Commands.CreateProduct;
using MS.Catalog.Infrastructure.Behaviours;
using MS.Catalog.Infrastructure.Data;
using Serilog;
using System.Reflection;

var configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration(x => x.AddConfiguration(configuration));
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSequentialGuidGenerator(SequentialGuidType.SequentialAsString);


builder.Services.AddCommandHandler(Assembly.Load("MS.Catalog.Application"));
builder.Services.AddInMemoryCommandDispatcher();
//builder.Services.AddCommandBehaviours();

builder.Services.AddQueryHandler(Assembly.Load("MS.Catalog.Application"));
builder.Services.AddInMemoryQueryDispatcher();

builder.Services.AddCatalogDbContext(builder.Configuration);
builder.Services.AddRepositories();

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();


var app = builder.Build();
//var routePrefix = $"/{AppName}";
//app.UsePathBase(routePrefix);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //c.RoutePrefix = routePrefix;
    });
}

//app.UseAuthentication();

//app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program
{
    public static string Namespace = "MS.Catalog.Api";
    public static string AppName = "Catalog";

    static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var config = builder.Build();
        return builder.Build();
    }
    static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {
        //var seqServerUrl = configuration["Serilog:SeqServerUrl"];
        //var logstashUrl = configuration["Serilog:LogstashgUrl"];
        return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProperty("ApplicationContext", Program.AppName)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            //.WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
            //.WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

}



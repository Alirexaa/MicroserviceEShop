using Core.Common.Cqrs;
using Core.Infrastructure.GuidGenerator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MS.Catalog.Infrastructure.Behaviours;
using MS.Catalog.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSequentialGuidGenerator();


builder.Services.AddCommandHandler();
builder.Services.AddInMemoryCommandDispatcher();
builder.Services.AddCommandBehaviours();

builder.Services.AddQueryHandler();
builder.Services.AddInMemoryQueryDispatcher();

builder.Services.AddCatalogDbContext(builder.Configuration);
builder.Services.AddRepositories();


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

app.UseAuthorization();

app.MapControllers();




app.Run();


public partial class Program
{
    public static string Namespace = "MS.Catalog.Api";
    public static string AppName = "Catalog";
}
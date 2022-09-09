using Core.Common.Cqrs;
using Core.Infrastructure.GuidGenerator;
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

builder.Services.AddQueryHandler();
builder.Services.AddInMemoryQueryDispatcher();

builder.Services.AddCatalogDbContext(builder.Configuration);
builder.Services.AddRepositories();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

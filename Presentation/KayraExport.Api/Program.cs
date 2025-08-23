using KayraExport.Api.Middlewares;
using KayraExport.Application;
using KayraExport.Infrastructure;
using KayraExport.Persistence;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Logger log = new LoggerConfiguration()
    .WriteTo.PostgreSQL(
        builder.Configuration.GetConnectionString("PostgreSql"), 
        "Logs", 
        needAutoCreateTable: true)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddPersistenceServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

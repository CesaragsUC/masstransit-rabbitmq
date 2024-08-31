using MassTransitRabbitMq;
using Serilog.Events;
using Serilog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MassTransit;
using Consumer.Configurations;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.QuartzJobServices(builder.Configuration);
builder.Services.MassTransitServices(builder.Configuration);
builder.Services.HealthCheckServices();



var app = builder.Build();


app.UseRouting();


app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = HealthCheckResponseWriter
});

app.MapHealthChecks("/health/live", new HealthCheckOptions { ResponseWriter = HealthCheckResponseWriter });
app.MapControllers();

app.Run();


static Task HealthCheckResponseWriter(HttpContext context, HealthReport result)
{
    context.Response.ContentType = "application/json";
    return context.Response.WriteAsync(result.ToJsonString());
}
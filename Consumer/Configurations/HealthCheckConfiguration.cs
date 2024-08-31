using MassTransit;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Consumer.Configurations;

public static class HealthCheckConfiguration
{
    public static Task HealthCheckResponseWriter(HttpContext context, HealthReport result)
    {
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result.ToJsonString());
    }

}

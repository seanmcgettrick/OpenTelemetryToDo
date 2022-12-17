using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenTelemetryToDo.Service.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddTelemetry(this IServiceCollection services, string serviceName)
    {
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName);

        services.AddOpenTelemetry().WithTracing(builder =>
        {
            builder
                .AddSource(serviceName)
                .SetResourceBuilder(resourceBuilder)
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddConsoleExporter();
        }).WithMetrics(builder =>
        {
            builder
                .AddMeter(serviceName)
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter();
        }).StartWithHost();

        return services;
    }

    public static ILoggingBuilder AddTelemetry(this ILoggingBuilder logging, string serviceName)
    {
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName);

        logging.AddOpenTelemetry(builder =>
        {
            builder
                .SetResourceBuilder(resourceBuilder)
                .AddConsoleExporter();
        });

        return logging;
    }
}
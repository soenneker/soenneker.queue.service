using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Queue.Service.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Queue.Service.Registrars;

/// <summary>
/// Registers Azure Queue Storage service-client access.
/// </summary>
public static class QueueServiceUtilRegistrar
{
    /// <summary>
    /// Registers one queue-service utility for the application.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddQueueServiceUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IQueueServiceUtil, QueueServiceUtil>();

        return services;
    }

    /// <summary>
    /// Registers one queue-service utility per dependency-injection scope while retaining the singleton HTTP transport cache.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddQueueServiceUtilAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IQueueServiceUtil, QueueServiceUtil>();

        return services;
    }
}

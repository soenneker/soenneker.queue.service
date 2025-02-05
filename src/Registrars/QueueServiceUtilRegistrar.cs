using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Queue.Service.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Queue.Service.Registrars;

/// <summary>
/// A utility library for Azure Queue (Storage) service accessibility
/// </summary>
public static class QueueServiceUtilRegistrar
{
    /// <summary>
    /// Recommended
    /// </summary>
    public static IServiceCollection AddQueueServiceUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton().TryAddSingleton<IQueueServiceUtil, QueueServiceUtil>();

        return services;
    }

    public static IServiceCollection AddQueueServiceUtilAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton().TryAddScoped<IQueueServiceUtil, QueueServiceUtil>();

        return services;
    }
}
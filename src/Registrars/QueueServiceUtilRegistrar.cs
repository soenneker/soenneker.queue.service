using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Queue.Service.Abstract;

namespace Soenneker.Queue.Service.Registrars;

/// <summary>
/// A utility library for Azure Queue (Storage) service accessibility
/// </summary>
public static class QueueServiceUtilRegistrar
{
    /// <summary>
    /// Recommended
    /// </summary>
    public static void AddQueueServiceUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IQueueServiceUtil, QueueServiceUtil>();
    }

    public static void AddQueueServiceUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IQueueServiceUtil, QueueServiceUtil>();
    }
}
using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;

namespace Soenneker.Queue.Service.Abstract;

/// <summary>
/// Provides a lazily created Azure Queue Storage service client.
/// </summary>
public interface IQueueServiceUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the configured queue service client.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initial client creation.</param>
    /// <returns>The cached queue service client.</returns>
    [Pure]
    ValueTask<QueueServiceClient> Get(CancellationToken cancellationToken = default);
}

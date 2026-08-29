using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;

namespace Soenneker.Queue.Service.Abstract;

/// <summary>
/// A utility library for Azure Queue (Storage) service client (QueueServiceClient) accessibility <para/>
/// Singleton IoC recommended
/// </summary>
public interface IQueueServiceUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured queue Service Client used by the Queue Service.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested queue Service Client.</returns>
    [Pure]
    ValueTask<QueueServiceClient> Get(CancellationToken cancellationToken = default);
}

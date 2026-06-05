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
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    [Pure]
    ValueTask<QueueServiceClient> Get(CancellationToken cancellationToken = default);
}
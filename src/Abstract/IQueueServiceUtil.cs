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
    [Pure]
    ValueTask<QueueServiceClient> Get(CancellationToken cancellationToken = default);
}
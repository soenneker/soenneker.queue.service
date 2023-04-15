using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Soenneker.Extensions.Configuration;
using Soenneker.Queue.Service.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Queue.Service;

///<inheritdoc cref="IQueueServiceUtil"/>
public class QueueServiceUtil : IQueueServiceUtil
{
    private readonly AsyncSingleton<QueueServiceClient> _client;
    private readonly AsyncSingleton<HttpClient> _httpClient;

    public QueueServiceUtil(IConfiguration config)
    {
        _httpClient = new AsyncSingleton<HttpClient>(() =>
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                MaxConnectionsPerServer = 20
            };

            var httpClient = new HttpClient(socketsHandler);

            return httpClient;
        });

        _client = new AsyncSingleton<QueueServiceClient>(async () =>
        {
            var clientOptions = new QueueClientOptions
            {
                Transport = new HttpClientTransport(await _httpClient.Get())
            };

            var connectionString = config.GetValueStrict<string>("Azure:Storage:Queue:ConnectionString");

            var client = new QueueServiceClient(connectionString, clientOptions);

            return client;
        });
    }

    public ValueTask<QueueServiceClient> GetClient()
    {
        return _client.Get();
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        await _httpClient.DisposeAsync().ConfigureAwait(false);

        await _client.DisposeAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _client.Dispose();
        _httpClient.Dispose();
    }
}
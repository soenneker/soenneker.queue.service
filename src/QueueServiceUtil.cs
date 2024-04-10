using System;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Queue.Service.Abstract;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Queue.Service;

///<inheritdoc cref="IQueueServiceUtil"/>
public class QueueServiceUtil : IQueueServiceUtil
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly AsyncSingleton<QueueServiceClient> _client;

    public QueueServiceUtil(IConfiguration config, IHttpClientCache httpClientCache)
    {
        _httpClientCache = httpClientCache;

        _client = new AsyncSingleton<QueueServiceClient>(async () =>
        {
            var clientOptions = new QueueClientOptions
            {
                Transport = new HttpClientTransport(await httpClientCache.Get(nameof(QueueServiceClient)))
            };

            var connectionString = config.GetValueStrict<string>("Azure:Storage:Queue:ConnectionString");

            var client = new QueueServiceClient(connectionString, clientOptions);

            return client;
        });
    }

    public ValueTask<QueueServiceClient> Get()
    {
        return _client.Get();
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        await _httpClientCache.Remove(nameof(QueueServiceClient)).NoSync();

        await _client.DisposeAsync().NoSync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClientCache.RemoveSync(nameof(QueueServiceClient));

        _client.Dispose();
    }
}
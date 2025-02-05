using System;
using System.Net.Http;
using System.Threading;
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

        _client = new AsyncSingleton<QueueServiceClient>(async (token, _) =>
        {
            var connectionString = config.GetValueStrict<string>("Azure:Storage:Queue:ConnectionString");

            HttpClient httpClient = await httpClientCache.Get(nameof(QueueServiceClient), cancellationToken: token).NoSync();

            var clientOptions = new QueueClientOptions
            {
                Transport = new HttpClientTransport(httpClient)
            };

            return new QueueServiceClient(connectionString, clientOptions);
        });
    }

    public ValueTask<QueueServiceClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
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
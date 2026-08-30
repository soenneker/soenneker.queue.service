[![](https://img.shields.io/nuget/v/Soenneker.Queue.Service.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Queue.Service/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.queue.service/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.queue.service/actions/workflows/publish-package.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.queue.service/build-and-test.yml?label=Build&style=for-the-badge)](https://github.com/soenneker/soenneker.queue.service/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Queue.Service.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Queue.Service/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.queue.service/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.queue.service/actions/workflows/codeql.yml)

# Soenneker.Queue.Service

Provides a lazily created, cached Azure Queue Storage `QueueServiceClient` through dependency injection.

Use this package for account-level operations such as listing queues and retrieving service properties. Use `Soenneker.Queue.Client` when code needs a client for a named queue.

## Install

```bash
dotnet add package Soenneker.Queue.Service
```

## Configuration

```json
{
  "Azure": {
    "Storage": {
      "Queue": {
        "ConnectionString": "<Azure Storage connection string>"
      }
    }
  }
}
```

The connection string is read the first time the service client is requested and remains associated with that utility instance.

## Registration

```csharp
using Soenneker.Queue.Service.Registrars;

builder.Services.AddQueueServiceUtilAsSingleton();
```

Singleton registration shares the lazily created `QueueServiceClient` across the application. Scoped registration is available when the wrapper must follow a request or operation scope:

```csharp
builder.Services.AddQueueServiceUtilAsScoped();
```

The HTTP transport cache remains singleton-owned with either registration. Disposing a scoped wrapper does not evict the transport used by other scopes.

Both registration methods use `TryAdd`; an existing `IQueueServiceUtil` registration is preserved.

## Usage

```csharp
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Soenneker.Queue.Service.Abstract;

public sealed class QueueCatalog(IQueueServiceUtil queueService)
{
    public async Task<List<string>> List(CancellationToken cancellationToken)
    {
        QueueServiceClient client = await queueService.Get(cancellationToken);
        var names = new List<string>();

        await foreach (QueueItem queue in client.GetQueuesAsync(cancellationToken: cancellationToken))
            names.Add(queue.Name);

        return names;
    }
}
```

The DI container owns registered utilities. Application code should not dispose an injected instance manually.

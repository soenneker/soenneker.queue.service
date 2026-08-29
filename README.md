[![](https://img.shields.io/nuget/v/Soenneker.Queue.Service.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Queue.Service/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.queue.service/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.queue.service/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Queue.Service.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Queue.Service/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.queue.service/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.queue.service/actions/workflows/codeql.yml)

# Soenneker.Queue.Service

A utility library for Azure Queue (Storage) service client (QueueServiceClient) accessibility Singleton IoC recommended.

## Install

```bash
dotnet add package Soenneker.Queue.Service
```

## Quick start

```csharp
using Soenneker.Queue.Service.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddQueueServiceUtilAsSingleton();
```

Recommended.

## What you get

- `IQueueServiceUtil` — A utility library for Azure Queue (Storage) service client (QueueServiceClient) accessibility Singleton IoC recommended.
- `QueueServiceUtilRegistrar` — A utility library for Azure Queue (Storage) service accessibility.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `QueueServiceUtilRegistrar.AddQueueServiceUtilAsSingleton(services)` | Recommended. | The same service collection, so additional registrations can be chained. |
| `QueueServiceUtilRegistrar.AddQueueServiceUtilAsScoped(services)` | Registers Queue Service Util with a scoped lifetime. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.

using Soenneker.Queue.Service.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Queue.Service.Tests;

[Collection("Collection")]
public class QueueServiceUtilTests : FixturedUnitTest
{
    private readonly IQueueServiceUtil _util;

    public QueueServiceUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IQueueServiceUtil>(true);
    }
}

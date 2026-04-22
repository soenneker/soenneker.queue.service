using Soenneker.Queue.Service.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Queue.Service.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class QueueServiceUtilTests : HostedUnitTest
{
    private readonly IQueueServiceUtil _util;

    public QueueServiceUtilTests(Host host) : base(host)
    {
        _util = Resolve<IQueueServiceUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}

using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Messaging
{
    public class MockQueueReaderService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return null;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return null;
        }
    }
}

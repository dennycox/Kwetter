using ProfileService.Interfaces;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Messaging
{
    public class MockSubscriber : ISubscriber
    {
        public void Dispose()
        {
        }

        public void Subscribe(Func<BasicDeliverEventArgs, bool> callback)
        {
        }
    }
}

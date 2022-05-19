using ProfileService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Messaging
{
    public class MockPublisher : IPublisher
    {
        public MockPublisher()
        {
        }

        public void Dispose()
        {
        }

        public void Publish(string message, string routingKey, IDictionary<string, object> messageAttributes, string timeToLive = null)
        {
        }
    }
}

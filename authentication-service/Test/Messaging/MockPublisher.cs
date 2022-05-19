using System.Collections.Generic;
using AuthenticationService.Interfaces;

namespace Test.Messaging
{
    public class MockPublisher : IPublisher
    {
        public MockPublisher(IConnectionProvider connectionProvider)
        {
        }

        public void Publish(string message, string routingKey, IDictionary<string, object> messageAttributes, string timeToLive = null)
        {
            
        }
    }
}

using AuthenticationService.Interfaces;
using RabbitMQ.Client;

namespace Test.Messaging
{
    public class MockConnectionProvider : IConnectionProvider
    {

        public MockConnectionProvider()
        {
        }

        public IModel CreateChannel()
        {
            return null;
        }

        public void Dispose()
        {
            
        }

        public IConnection GetConnection()
        {
            return null;
        }
    }
}

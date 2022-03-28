using System;
using RabbitMQ.Client;

namespace AuthenticationService.Interfaces
{
    public interface IConnectionProvider : IDisposable
    {
        IModel CreateChannel();
        IConnection GetConnection();
    }
}

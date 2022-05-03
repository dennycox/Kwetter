using System;
using RabbitMQ.Client;

namespace ProfileService.Interfaces
{
    public interface IConnectionProvider : IDisposable
    {
        IModel CreateChannel();
        IConnection GetConnection();
    }
}

using System;
using ProfileService.Interfaces;
using RabbitMQ.Client;

namespace ProfileService.Messaging
{
    public class RabbitMqConnection : IConnectionProvider
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private bool _disposed;

        public RabbitMqConnection(string url)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(url)
            };
            _connection = _factory.CreateConnection();
        }

        public IModel CreateChannel()
        {
            var connection = GetConnection();
            return connection.CreateModel();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _connection?.Close();

            _disposed = true;
        }
    }
}

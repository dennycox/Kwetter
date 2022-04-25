using System;
using RabbitMQ.Client.Events;

namespace ProfileService.Interfaces
{
    public interface ISubscriber : IDisposable
    {
        void Subscribe(Func<BasicDeliverEventArgs, bool> callback);
    }
}

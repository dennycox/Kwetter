using System;
using RabbitMQ.Client.Events;

namespace AuthenticationService.Interfaces
{
    public interface ISubscriber : IDisposable
    {
        void Subscribe(Func<BasicDeliverEventArgs, bool> callback);
    }
}

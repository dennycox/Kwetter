using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfileService.Interfaces;
using RabbitMQ.Client.Events;

namespace ProfileService.Messaging
{
    public class QueueReaderService : IHostedService
    {
        private readonly ISubscriber _subscriber;
        private readonly IServiceProvider _serviceProvider;
        private readonly MessageHandlerRepository _messageHandlerRepository;

        public QueueReaderService(ISubscriber subscriber, IServiceProvider serviceProvider, MessageHandlerRepository messageHandlerRepository)
        {
            _subscriber = subscriber;
            _serviceProvider = serviceProvider;
            _messageHandlerRepository = messageHandlerRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Subscribe(HandleMessage);
            return Task.CompletedTask;
        }

        private bool HandleMessage(BasicDeliverEventArgs message)
        {
            var messageHandler = _messageHandlerRepository.TryGetHandlerForMessageType(message.RoutingKey);
            if (messageHandler != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var body = message.Body.ToArray();
                    messageHandler.HandleMessageAsync(message.RoutingKey, body).GetAwaiter().GetResult();
                }
            }
            return true;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

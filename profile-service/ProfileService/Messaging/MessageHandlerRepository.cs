using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Interfaces;

namespace ProfileService.Messaging
{
    public class MessageHandlerRepository
    {
        private readonly IDictionary<string, IMessageHandler> _messageHandlers;

        public MessageHandlerRepository(IServiceProvider serviceProvider)
        {
            _messageHandlers = new Dictionary<string, IMessageHandler>();

            var scope = serviceProvider.CreateScope();

            IMessageHandler accountCreatedMessageHandler = new AccountCreatedMessageHandler(scope.ServiceProvider.GetRequiredService<IProfileRepository>());
            _messageHandlers.Add("account.new", accountCreatedMessageHandler);
        }

        public IMessageHandler TryGetHandlerForMessageType(string messageType)
        {
            if (_messageHandlers.ContainsKey(messageType)) return _messageHandlers[messageType];
            else return null;
        }
    }
}

using System;
using System.Collections.Generic;
using AuthenticationService.Data;
using AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Messaging
{
    public class MessageHandlerRepository
    {
        private readonly IDictionary<string, IMessageHandler> _messageHandlers;

        public MessageHandlerRepository(IServiceProvider serviceProvider)
        {
            _messageHandlers = new Dictionary<string, IMessageHandler>();

            var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            IMessageHandler profileUpdatedMessageHandler = new ProfileUpdatedMessageHandler(userManager);
            _messageHandlers.Add("profile.update", profileUpdatedMessageHandler);

            IMessageHandler profileDeletedMessageHandler = new ProfileDeletedMessageHandler(userManager);
            _messageHandlers.Add("profile.delete", profileDeletedMessageHandler);
        }

        public IMessageHandler TryGetHandlerForMessageType(string messageType)
        {
            if (_messageHandlers.ContainsKey(messageType)) return _messageHandlers[messageType];
            else return null;
        }
    }
}

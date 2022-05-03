using System;
using System.Text.Json;
using System.Threading.Tasks;
using AuthenticationService.Interfaces;

namespace AuthenticationService.Messaging
{
    public abstract class MessageHandler<TMessage> : IMessageHandler where TMessage : class
    {
        Task IMessageHandler.HandleMessageAsync(string messageType, byte[] obj)
        {
            return HandleMessageAsync(messageType, JsonSerializer.Deserialize<TMessage>(obj));
        }

        public abstract Task HandleMessageAsync(string messageType, TMessage message);
    }
}

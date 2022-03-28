using System;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(string messageType, byte[] obj);
    }
}

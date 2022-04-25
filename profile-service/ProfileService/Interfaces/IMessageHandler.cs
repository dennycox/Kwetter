using System;
using System.Threading.Tasks;

namespace ProfileService.Interfaces
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(string messageType, byte[] obj);
    }
}

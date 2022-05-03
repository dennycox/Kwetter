using System;
using System.Collections.Generic;

namespace AuthenticationService.Interfaces
{
    public interface IPublisher
    {
        void Publish(string message, string routingKey, IDictionary<string, object> messageAttributes, string timeToLive = null);
    }
}

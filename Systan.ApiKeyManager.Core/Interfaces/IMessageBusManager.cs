using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Interfaces
{
    public interface IMessageBusManager
    {
        Task Subscribe(string subscribeUrl, SubscribeRequest request);
        Task OnMessage(BaseBusMessage message);
    }
}

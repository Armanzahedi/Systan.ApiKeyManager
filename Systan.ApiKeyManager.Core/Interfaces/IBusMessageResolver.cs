using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Interfaces
{
    public interface IBusMessageResolver
    {
        Task ResolveMessage(BaseBusMessage message);
    }
}

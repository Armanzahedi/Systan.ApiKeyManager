using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class SubscribeRequest
    {
        public string ServiceId { get; set; } = null!;
        public string Endpoint { get; set; } = null!;
        public string Subject { get; set; } = null!;

    }
}

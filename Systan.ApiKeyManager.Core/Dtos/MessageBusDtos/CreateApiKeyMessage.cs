using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class CreateApiKeyBody
    {
        public string? ApiKeyId { get; set; }
        public string? ApiKey { get; set; }
    }
    public class CreateApiKeyMessage : BaseBusMessage
    {
        public new CreateApiKeyBody? Body { get; set; }
    }
}

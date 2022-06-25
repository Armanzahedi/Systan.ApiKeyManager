using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class UpdateApiKeyBody
    {
        public string? OldApiKey { get; set; }
        public string? NewApiKey { get; set; }
        public string? ApiKeyId { get; set; }
    }
    public class UpdateApiKeyMessage : BaseBusMessage
    {
        public new UpdateApiKeyBody? Body { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class CreateApiKeySettingBody
    {
        public string? ApiKeySettingId { get; set; }
        public string? ApiKeyId { get; set; }
        public string? ApiKey { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
    public class CreateApiKeySettingMessage : BaseBusMessage
    {
        public new CreateApiKeySettingBody? Body { get; set; }
    }
}

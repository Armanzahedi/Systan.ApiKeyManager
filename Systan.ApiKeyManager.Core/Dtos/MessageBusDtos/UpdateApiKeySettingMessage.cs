using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class UpdateApiKeySettingBody
    {
        public string? ApiKeySettingId { get; set; }
        public string? ApiKeyId { get; set; }
        public string? ApiKey { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
    public class UpdateApiKeySettingMessage : BaseBusMessage
    {
        public new UpdateApiKeySettingBody? Body { get; set; }
    }
}

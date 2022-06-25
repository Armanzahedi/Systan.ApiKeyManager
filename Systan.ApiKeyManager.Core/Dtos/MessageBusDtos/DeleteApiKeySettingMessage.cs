using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class DeleteApiKeySettingBody
    {
        public string? ApiKeySettingId { get; set; }
        public string? ApiKeyId { get; set; }
        public string? Key { get; set; }
    }
    public class DeleteApiKeySettingMessage : BaseBusMessage
    {
        public new DeleteApiKeySettingBody? Body { get; set; }
    }
}

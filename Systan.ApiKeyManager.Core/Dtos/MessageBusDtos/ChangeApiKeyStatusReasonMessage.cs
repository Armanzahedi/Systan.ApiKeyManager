using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class ChangeApiKeyStatusReasonBody
    {
        public string? ApiKeyId { get; set; }
        public string? ApiKey { get; set; }
        public int? StatusReason { get; set; }
        public string? StatusReasonTitle { get; set; }
    }

    public class ChangeApiKeyStatusReasonMessage : BaseBusMessage
    {
        public new ChangeApiKeyStatusReasonBody? Body { get; set; }
    }
}

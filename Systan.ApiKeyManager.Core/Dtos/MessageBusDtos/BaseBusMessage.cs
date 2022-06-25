using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.MessageBusDtos
{
    public class BaseBusMessage
    {
        public string? Subject { get; set; }
        public virtual JsonObject? Body { get; set; }
        public string? ServiceId { get; set; }
    }
}

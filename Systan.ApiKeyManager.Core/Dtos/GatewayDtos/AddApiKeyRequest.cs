using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.GatewayDtos
{
    public class AddApiKeyRequest
    {
        public string id { get; set; } = null!;
        public string key { get; set; } = null!;
        public List<string>? tags{ get; set; }
    }
}

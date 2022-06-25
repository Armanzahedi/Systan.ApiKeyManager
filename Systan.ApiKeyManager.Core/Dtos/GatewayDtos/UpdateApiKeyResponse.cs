using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.GatewayDtos
{
    public class UpdateApiKeyResponse
    {
        public string? id { get; set; }
        public int created_at { get; set; }
        public string? key { get; set; }
        public string? ttl { get; set; }
        public List<string>? tags { get; set; }
        public ConsumerDto? consumer { get; set; }
    }
}

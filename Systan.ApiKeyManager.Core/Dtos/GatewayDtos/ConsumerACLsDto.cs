using Systan.ApiKeyManager.Core.Dtos.GatewayDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.GatewayDtos
{
    public class ConsumerACLsDto
    {
        public List<ConsumerACLDto>? data { get; set; }
    }

    public class ConsumerACLDto
    {
        public string id { get; set; } = null!;
        public List<string>? tags { get; set; }
        public string group { get; set; } = null!;
        public int created_at { get; set; }
        public ConsumerDto? consumer { get; set; }
    }
}

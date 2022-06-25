using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.GatewayDtos
{
    public class UpdateConsumerRequest
    {
        public string? username { get; set; }
        public string? custom_id { get; set; }
        public List<string>? tags { get; set; }
    }
}

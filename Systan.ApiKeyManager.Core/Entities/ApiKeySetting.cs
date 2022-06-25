using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Entities
{
    public class ApiKeySetting : BaseEntity
    {
        public string SystanId { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string? Value { get; set; }

        public int ApiKeyId { get; set; }
        public ApiKey ApiKey { get; set; } = null!;
    }
}

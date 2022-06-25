using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Entities
{
    public class ApiKey : BaseEntity
    {
        public string SystanId { get; set; } = null!;
        public string Key { get; set; } = null!;

        public int? StatusReason { get; set; }
        public string? StatusReasonTitle { get; set; }

        public string? ServiceId { get; set; }
        public ICollection<ApiKeySetting>? ApiKeySettings { get; set; }
    }
}

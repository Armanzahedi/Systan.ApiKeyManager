using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Entities
{
    public class Service : BaseEntity
    {
        public string SystanId { get; set; } = null!;
        public string Name { get; set; } = null!;
        
        public ICollection<ApiKey>? ApiKeys { get; set; }
    }
}

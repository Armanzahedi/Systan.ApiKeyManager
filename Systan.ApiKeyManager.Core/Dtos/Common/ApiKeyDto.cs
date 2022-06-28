using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.Common
{
    public class ApiKeyDto
    {
        public string ApiKey { get; set; } = null!;
        public List<ApiKeySettingDto>? Settings { get; set; }
    }
    public class ApiKeySettingDto
    {
        public string? Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}

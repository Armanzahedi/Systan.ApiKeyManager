using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Dtos.Common
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
        }
        public BaseResponse(T? data)
        {
            Succeeded = true;
            Message = string.Empty;
            Data = data;
        }
        public T? Data { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
    }
}

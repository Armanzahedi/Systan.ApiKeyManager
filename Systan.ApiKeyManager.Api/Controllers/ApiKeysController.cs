using Microsoft.AspNetCore.Mvc;
using Systan.ApiKeyManager.Core.Dtos.Common;
using Systan.ApiKeyManager.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Systan.ApiKeyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeysController : ControllerBase
    {
        private readonly IApiKeyRepository _apikeyRepo;

        public ApiKeysController(IApiKeyRepository apikeyRepo)
        {
            _apikeyRepo = apikeyRepo;
        }

        [HttpGet("{key}")]
        public async Task<BaseResponse<ApiKeyDto>> Get(string key)
        {
            var apikey = _apikeyRepo.GetWithSettings(key);
            return null;
        }

        // POST api/<ApiKeysController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiKeysController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiKeysController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

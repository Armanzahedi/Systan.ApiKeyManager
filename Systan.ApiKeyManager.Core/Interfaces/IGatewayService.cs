using Systan.ApiKeyManager.Core.Dtos.GatewayDtos;


namespace Systan.ApiKeyManager.Core.Interfaces
{
    public interface IGatewayService
    {
        Task<AddApiKeyResponse> AddApiKey(AddApiKeyRequest request);
        Task<UpdateApiKeyResponse> UpdateApiKey(string apiKeySystanId, UpdateApiKeyRequest request);
        Task DeleteApiKey(string apiKeySystanId);
        Task DeactivateApiKey(string apiKeySystanId);
        Task ActivateApiKey(string apiKeySystanId);

    }
}

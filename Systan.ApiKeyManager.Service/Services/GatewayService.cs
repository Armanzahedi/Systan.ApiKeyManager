using Microsoft.Extensions.Configuration;
using RestSharp;
using Systan.ApiKeyManager.Core.Dtos.GatewayDtos;
using Systan.ApiKeyManager.Core.Interfaces;

namespace Systan.ApiKeyManager.Service.Services
{
    public class GatewayService : IGatewayService
    {
        private IConfiguration _configuration;
        private string _baseUrl;
        private Dictionary<string, string> _defaultHeaders;
        private RestClient _client;

        public GatewayService(IConfiguration configuration)
        {
            _configuration = configuration;
            this._baseUrl = _configuration["Gateway:Api"];

            var defaultHeaders = new Dictionary<string, string>();
            defaultHeaders.Add("apikey", _configuration["Gateway:ApiKey"]);
            this._defaultHeaders = defaultHeaders;

            this._client = new RestClient(_baseUrl).AddDefaultHeaders(_defaultHeaders);

        }

        public async Task<AddApiKeyResponse> AddApiKey(AddApiKeyRequest request)
        {
            var consumer = await AddConsumer(new AddConsumerRequest
            {
                id = request.id,
                custom_id = request.key
            });
            var req = new RestRequest("consumers/{consumerId}/key-auth")
            .AddUrlSegment("consumerId", consumer.id!)
            .AddJsonBody(request);

            var res = this._client.ExecutePost<AddApiKeyResponse>(req);
            if (res.IsSuccessful)
                return res.Data;
            else
                throw new HttpRequestException(res.Content);
        }

        public async Task<UpdateApiKeyResponse> UpdateApiKey(string apiKeySystanId, UpdateApiKeyRequest request)
        {
            var consumer = await UpdateConsumer(apiKeySystanId, new UpdateConsumerRequest
            {
                custom_id = request.key
            });
            var req = new RestRequest("consumers/{consumerId}/key-auth/{apiKeyId}")
              .AddUrlSegment("consumerId", consumer.id!)
              .AddUrlSegment("apiKeyId", apiKeySystanId)
              .AddJsonBody(request);

            var res = this._client.ExecutePut<UpdateApiKeyResponse>(req);
            if (res.IsSuccessful)
                return res.Data;
            else
                throw new HttpRequestException(res.Content);
        }

        public async Task DeleteApiKey(string apiKeySystanId)
        {
            var req = new RestRequest("consumers/{consumerId}/key-auth/{apiKeyId}")
              .AddUrlSegment("consumerId", apiKeySystanId)
              .AddUrlSegment("apiKeyId", apiKeySystanId);
            await this._client.DeleteAsync(req);

            await this.DeleteConsumer(apiKeySystanId);
        }


        public async Task DeactivateApiKey(string apiKeySystanId)
        {
            var consumerACLs = await this.GetConsumerACLs(apiKeySystanId);
            if (consumerACLs?.data != null && consumerACLs.data.Any(d => d.group.ToLower().Equals("inactive")) == false)
            {
                var req = new RestRequest("consumers/{consumerId}/acls")
                  .AddUrlSegment("consumerId", apiKeySystanId)
                  .AddJsonBody(new
                  {
                      group = "Inactive"
                  });
                var res = this._client.ExecutePost(req);
                if (res.IsSuccessful == false)
                    throw new HttpRequestException(res.Content);
            }


            var consumer = await this.GetConsumer(apiKeySystanId);
            if (consumer == null)
                throw new HttpRequestException("Consumer was not found");

            if (consumer.tags == null || consumer.tags.Any(t => t == "Inactive") == false)
            {
                var tags = consumer.tags ?? new List<string>();
                tags.Add("Inactive");
                consumer.tags = tags;
                await this.UpdateConsumer(apiKeySystanId, new UpdateConsumerRequest
                {
                    custom_id = consumer.custom_id,
                    tags = consumer.tags,
                });
            }


        }

        public async Task ActivateApiKey(string apiKeySystanId)
        {
            var consumerACLs = await this.GetConsumerACLs(apiKeySystanId);

            if (consumerACLs?.data != null && consumerACLs.data.Any(d => d.group.ToLower().Equals("inactive")))
            {
                var inactiveACL = consumerACLs.data.First(d => d.group.ToLower().Equals("inactive"));

                var req = new RestRequest("consumers/{id}/acls/{aclId}")
                 .AddUrlSegment("id", apiKeySystanId)
                 .AddUrlSegment("aclId", inactiveACL.id);
                await this._client.DeleteAsync(req);
            }


            var consumer = await this.GetConsumer(apiKeySystanId);
            if (consumer == null)
                throw new HttpRequestException("Consumer was not found");

            if (consumer.tags != null && consumer.tags.Any(t => t == "Inactive"))
            {
                consumer.tags.Remove("Inactive");
                await this.UpdateConsumer(apiKeySystanId, new UpdateConsumerRequest
                {
                    custom_id = consumer.custom_id,
                    tags = consumer.tags,
                });
            }

        }

        #region Private

        private async Task<AddConsumerResponse> AddConsumer(AddConsumerRequest request)
        {

            var req = new RestRequest("consumers")
            .AddJsonBody(request);

            var res = this._client.ExecutePost<AddConsumerResponse>(req);
            if (res.IsSuccessful)
                return res.Data;
            else
                throw new HttpRequestException(res.Content);
        }

        private async Task<UpdateConsumerResponse> UpdateConsumer(string consumerSystanId, UpdateConsumerRequest request)
        {
            var req = new RestRequest("consumers/{id}")
                .AddUrlSegment("id", consumerSystanId)
             .AddJsonBody(request);

            var res = this._client.ExecutePut<UpdateConsumerResponse>(req);
            if (res.IsSuccessful)
                return res.Data;
            else
                throw new HttpRequestException(res.Content);
        }
        private async Task DeleteConsumer(string consumerSystanId)
        {
            var req = new RestRequest("consumers/{id}")
                .AddUrlSegment("id", consumerSystanId);

            await this._client.DeleteAsync(req);
        }
        private async Task<GetConsumerResponse> GetConsumer(string consumerSystanId)
        {
            var req = new RestRequest("consumers/{id}")
                .AddUrlSegment("id", consumerSystanId);

            return await this._client.GetAsync<GetConsumerResponse>(req);
        }
        private async Task<ConsumerACLsDto> GetConsumerACLs(string consumerSystanId)
        {
            var req = new RestRequest("consumers/{id}/acls")
                .AddUrlSegment("id", consumerSystanId);

            return await this._client.GetAsync<ConsumerACLsDto>(req);
        }
        #endregion
    }
}

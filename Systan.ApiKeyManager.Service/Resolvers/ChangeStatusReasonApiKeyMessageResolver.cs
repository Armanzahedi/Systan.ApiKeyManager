using AutoMapper;
using Systan.ApiKeyManager.Core.Dtos.GatewayDtos;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Enums;
using Systan.ApiKeyManager.Core.Helpers;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Systan.ApiKeyManager.Service.Resolvers
{
    [BusMessage(BusMessages.CHANGESTATUSREASON_APIKEY)]
    public class ChangeStatusReasonApiKeyMessageResolver : IBusMessageResolver
    {
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public ChangeStatusReasonApiKeyMessageResolver(IApiKeyRepository apiKeyRepo,
            IGatewayService gatewayService,
            IMapper mapper)
        {
            _apiKeyRepo = apiKeyRepo;
            _gatewayService = gatewayService;
            _mapper = mapper;
        }

        public async Task ResolveMessage(BaseBusMessage baseMessage)
        {
            try
            {
                ChangeApiKeyStatusReasonMessage message = _mapper.Map<ChangeApiKeyStatusReasonMessage>(baseMessage);

                if (message.Body == null || message.Body.ApiKeyId == null || message.Body.StatusReason == null)
                    throw new Exception("Invalid Message Body.");

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var model = await _apiKeyRepo.GetBySystanId(message.Body.ApiKeyId);
                    if (model == null)
                        throw new Exception("ApiKey was not found.");

                    model.StatusReason = message.Body.StatusReason;
                    model.StatusReasonTitle = message.Body.StatusReasonTitle;
                    await _apiKeyRepo.Update(model);

                    switch (model.StatusReason)
                    {
                        case (int)ApiKeyStatusReasons.Active:
                            await _gatewayService.ActivateApiKey(message.Body.ApiKeyId);
                            break;
                        case (int)ApiKeyStatusReasons.Inactive:
                            await _gatewayService.DeactivateApiKey(message.Body.ApiKeyId);
                            break;
                        default:
                            throw new Exception("Invalid ApiKey StatusReason");
                    }
                    ts.Complete();
                }


            }
            catch (Exception e)
            {
                throw new Exception($"Could not resolve Message: {e.Message}", e);
            }
        }
    }
}

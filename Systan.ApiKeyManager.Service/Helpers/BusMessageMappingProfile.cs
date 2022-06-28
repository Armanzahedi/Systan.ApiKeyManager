using AutoMapper;
using Newtonsoft.Json;
using Systan.ApiKeyManager.Core.Dtos.Common;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Service.Helpers
{
    public class BusMessageMappingProfile : Profile
    {
        public BusMessageMappingProfile()
        {
            CreateMap<BaseBusMessage, CreateApiKeyMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<CreateApiKeyBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, UpdateApiKeyMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<UpdateApiKeyBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, ChangeApiKeyStatusReasonMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<ChangeApiKeyStatusReasonBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, CreateApiKeySettingMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<CreateApiKeySettingBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, UpdateApiKeySettingMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<UpdateApiKeySettingBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, DeleteApiKeySettingMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<DeleteApiKeySettingBody>(src.Body!.ToString())));

            CreateMap<ApiKey, ApiKeyDto>()
            .ForMember(dest => dest.ApiKey, opt => opt.MapFrom(a => a.Key)
            ).ForMember(dest => dest.Settings, opt => opt.MapFrom(a => a.ApiKeySettings.Select(a => new ApiKeySettingDto { Id = a.SystanId, Key = a.Key, Value = a.Value }).ToList()));
        }
    }
}

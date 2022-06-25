using AutoMapper;
using Newtonsoft.Json;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
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
                .ForMember(dest=>dest.Body,opt=>opt.MapFrom(src=> JsonConvert.DeserializeObject<CreateApiKeyBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, UpdateApiKeyMessage>()
                .ForMember(dest=>dest.Body,opt=>opt.MapFrom(src=> JsonConvert.DeserializeObject<UpdateApiKeyBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, ChangeApiKeyStatusReasonMessage>()
                .ForMember(dest=>dest.Body,opt=>opt.MapFrom(src=> JsonConvert.DeserializeObject<ChangeApiKeyStatusReasonBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, CreateApiKeySettingMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<CreateApiKeySettingBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, UpdateApiKeySettingMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<UpdateApiKeySettingBody>(src.Body!.ToString())));

            CreateMap<BaseBusMessage, DeleteApiKeySettingMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<DeleteApiKeySettingBody>(src.Body!.ToString())));
        }
    }
}

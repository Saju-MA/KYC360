using AutoMapper;
using KYC360.Services.Models;
using KYC360.Services.Models.Dto;

namespace KYC360.Services;

public class MappingConfig
{
    public static MapperConfiguration Configuration()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<SecretUrl, SecretUrlDto>();
            config.CreateMap<SecretUrlDto, SecretUrl>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });
        return mappingConfig;
    }
}

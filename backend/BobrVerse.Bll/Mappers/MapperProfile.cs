using AutoMapper;
using BobrVerse.Common.Models.DTO.BobrLevel;
using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Dal.Entities;

namespace BobrVerse.Bll.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BobrProfile, MyBobrProfileDTO>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));

            CreateMap<BobrLevel, BobrLevelDTO>();

            CreateMap<UpdateBobrProfileDTO, BobrProfile>();
        }
    }
}
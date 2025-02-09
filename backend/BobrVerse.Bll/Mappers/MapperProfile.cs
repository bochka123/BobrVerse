using AutoMapper;
using BobrVerse.Common.Models.DTO.BobrLevel;
using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Common.Models.DTO.Quest;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;

namespace BobrVerse.Bll.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMapForBobrProfile();

            CreateMapForQuest();
        }

        public void CreateMapForBobrProfile()
        {
            CreateMap<BobrProfile, MyBobrProfileDTO>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));

            CreateMap<BobrLevel, BobrLevelDTO>();

            CreateMap<UpdateBobrProfileDTO, BobrProfile>();
        }

        public void CreateMapForQuest()
        {
            CreateMap<CreateQuestDTO, Quest>();

            CreateMap<Quest, AuthorQuestDTO>();
        }
    }
}
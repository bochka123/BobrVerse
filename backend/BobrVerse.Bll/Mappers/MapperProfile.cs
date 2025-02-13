using AutoMapper;
using BobrVerse.Common.Extenions;
using BobrVerse.Common.Models.DTO.BobrLevel;
using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Common.Models.DTO.Quest;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Common.Models.Quest.Enums;
using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;
using BobrVerse.Dal.Entities.Quest.Tasks;

namespace BobrVerse.Bll.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMapForBobrProfile();

            CreateMapForQuest();

            CreateMapForQuizTask();

            CreateMapForQuestRating();

            CreateMapForQuestResponse();
        }

        public void CreateMapForQuestResponse()
        {
            CreateMap<QuestResponse, QuestResponseDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
        }

        public void CreateMapForQuestRating()
        {
            CreateMap<CreateQuestRatingDTO, QuestRating>();

            CreateMap<QuestRating, QuestRatingDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.BobrProfile != null ? src.BobrProfile.Name : "Anonymous"))
                .ForMember(dest => dest.QuestTitle, opt => opt.MapFrom(src => src.Quest != null ? src.Quest.Title : "Unknown quest"));
        }

        public void CreateMapForBobrProfile()
        {
            CreateMap<BobrProfile, MyBobrProfileDTO>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));

            CreateMap<UpdateBobrProfileDTO, BobrProfile>();

            CreateMap<BobrProfile, AuthorDTO>();

            CreateMap<BobrLevel, BobrLevelDTO>();
        }

        public void CreateMapForQuest()
        {
            CreateMap<CreateQuestDTO, Quest>()
            .ForMember(dest => dest.TimeLimit, opt => opt.MapFrom(src =>
                src.TimeLimitInSeconds.HasValue ? TimeSpan.FromSeconds(src.TimeLimitInSeconds.Value) : (TimeSpan?)null));

            CreateMap<Quest, QuestDTO>()
                .ForMember(dest => dest.TimeLimitInSeconds, opt => opt.MapFrom(src =>
                    src.TimeLimit.HasValue ? (int?)src.TimeLimit.Value.TotalSeconds : null))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<QuestDTO, Quest>()
                .ForMember(dest => dest.TimeLimit,
                           opt => opt.MapFrom(src => src.TimeLimitInSeconds.HasValue
                               ? TimeSpan.FromSeconds(src.TimeLimitInSeconds.Value)
                               : (TimeSpan?)null))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<QuestStatusEnum>(src.Status)));


            CreateMap<Quest, ViewQuestDTO>()
                .IncludeBase<Quest, QuestDTO>();
        }

        public void CreateMapForQuizTask()
        {
            CreateMap<CreateTaskDTO, QuizTask>()
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => Enum.Parse<TaskTypeEnum>(src.TaskType)))
                .ForMember(dest => dest.TimeLimit, opt => opt.MapFrom(src => src.TimeLimitInSeconds.HasValue
                    ? TimeSpan.FromSeconds(src.TimeLimitInSeconds.Value)
                    : (TimeSpan?)null));

            CreateMap<ResourceDTO, Resource>()
                .ReverseMap();

            CreateMap<QuizTask, QuizTaskDTO>()
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.TaskType.ToString()))
                .ForMember(dest => dest.TimeLimitInSeconds, opt => opt.MapFrom(src =>
                    src.TimeLimit.HasValue ? (int?)src.TimeLimit.Value.TotalSeconds : null));

            CreateMap<QuizTaskDTO, QuizTask>()
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => Enum.Parse<TaskTypeEnum>(src.TaskType)))
                .ForMember(dest => dest.RequiredResources, opt => opt.Ignore());
        }
    }
}
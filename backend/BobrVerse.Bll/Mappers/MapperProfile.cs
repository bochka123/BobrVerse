using AutoMapper;
using BobrVerse.Common.Models.DTO.BobrLevel;
using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Common.Models.DTO.Quest;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;
using BobrVerse.Dal.Entities.Quest.Tasks;
using BobrVerse.Dal.Interfaces.Tasks;

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
        }

        public void CreateMapForQuestRating()
        {
            CreateMap<CreateQuestRatingDTO, QuestRating>();

            CreateMap<QuestRating, QuestRatingDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.BobrProfile != null ? src.BobrProfile.Name : "Anonymous"));
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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ToString()))
                .ReverseMap();
        }

        public void CreateMapForQuizTask()
        {
            CreateMap<CreateTaskDTO, QuizTask>()
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => Enum.Parse<TaskTypeEnum>(src.TaskType)))
                .ForMember(dest => dest.TimeLimit, opt => opt.MapFrom(src => src.TimeLimitInSeconds.HasValue
                    ? TimeSpan.FromSeconds(src.TimeLimitInSeconds.Value)
                    : (TimeSpan?)null));

            CreateMap<ResourceDTO, Resource>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Enum.Parse<ResourceNameEnum>(src.Name)))
                .ReverseMap();

            CreateMap<QuizTask, QuizTaskDTO>()
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.TaskType.ToString()))
                .ForMember(dest => dest.TimeLimitInSeconds, opt => opt.MapFrom(src =>
                    src.TimeLimit.HasValue ? (int?)src.TimeLimit.Value.TotalSeconds : null));

            CreateMap<QuizTaskDTO, QuizTask>()
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => Enum.Parse<TaskTypeEnum>(src.TaskType)))
                .ForMember(dest => dest.RequiredResources, opt => opt.Ignore());

            CreateMap<ICollectResourcesTask, QuizTaskDTO>()
                .Include<QuizTask, QuizTaskDTO>();

        }
    }
}
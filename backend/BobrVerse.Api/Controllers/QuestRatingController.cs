using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.Quest;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MainAuth]
    public class QuestRatingController(IQuestRatingService service) : ControllerBase
    {
        [HttpPost]
        public async Task<ApiResponse> Create(CreateQuestRatingDTO dto)
        {
            await service.CreateAsync(dto);
            return new ApiResponse(true);
        }

        [HttpGet("averageUserRating")]
        public async Task<ApiResponse<double>> GetAverageUserRating() =>
            new ApiResponse<double>(await service.GetAverageUserRatingAsync());

        [HttpGet("averageQuestRating/{questId:Guid}")]
        public async Task<ApiResponse> GetAverageQuestRating([FromRoute] Guid questId) =>
            new ApiResponse<double>(await service.GetAverageQuestRatingAsync(questId));

        [HttpGet("quest/{questId:Guid}/{start:int}/{end:int}")]
        public async Task<ApiResponse<ICollection<QuestRatingDTO>>> GetQuestRatings([FromRoute] Guid questId, [FromRoute] int start, [FromRoute] int end) =>
            new ApiResponse<ICollection<QuestRatingDTO>>(await service.GetQuestRatingsAsync(questId, start, end));

        [HttpGet("user/{start:int}/{end:int}")]
        public async Task<ApiResponse<ICollection<QuestRatingDTO>>> GetuserRatings([FromRoute] Guid questId, [FromRoute] int start, [FromRoute] int end) =>
            new ApiResponse<ICollection<QuestRatingDTO>>(await service.GetUserRatingsAsync(start, end));
    }
}
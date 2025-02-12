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
    public class QuestResponseController(IQuestResponseService service) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ApiResponse<QuestResponseDTO>> Create(Guid questId) 
            => new ApiResponse<QuestResponseDTO>(await service.CreateAsync(questId));

        [HttpGet("getUserQuestResponses/{start:int}/{end:int}")]
        public async Task<ApiResponse<ICollection<QuestResponseDTO>>> GetUserQuestResponses([FromRoute] int start, [FromRoute] int end)
            => new ApiResponse<ICollection<QuestResponseDTO>>(await service.GetUserQuestResponsesAsync(start, end));
    }
}
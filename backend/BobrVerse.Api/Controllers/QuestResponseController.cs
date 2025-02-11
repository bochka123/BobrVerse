using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces.Quest;
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
        public async Task<QuestResponseDTO> Create(Guid questId) 
            => await service.CreateAsync(questId);

        [HttpGet("getUserQuestResponses/{start:int}/{end:int}")]
        public async Task<ICollection<QuestResponseDTO>> GetUserQuestResponses([FromRoute] int start, [FromRoute] int end)
            => await service.GetUserQuestResponsesAsync(start, end);
    }
}
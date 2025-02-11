using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.Quest;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/[controller]")]
    [MainAuth]
    [ApiController]
    public class QuestController(IQuestService service) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ApiResponse<QuestDTO>> Create(CreateQuestDTO dto) => new ApiResponse<QuestDTO>(await service.CreateAsync(dto));

        [HttpGet("my")]
        public async Task<ApiResponse<ICollection<QuestDTO>>> GetMyQuests() => new ApiResponse<ICollection<QuestDTO>>(await service.GetMyQuests());
    }
}

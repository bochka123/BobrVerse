using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.Quest.Task;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MainAuth]
    public class QuestTaskReposponseController(IQuestTaskResponseService service) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ApiResponse<QuestTaskResponseDTO>> CreateAsync(CreateQuestTaskResponseDTO dto) => new ApiResponse<QuestTaskResponseDTO>(await service.AnswerAsync(dto));
    }
}

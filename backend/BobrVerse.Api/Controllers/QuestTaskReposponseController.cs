using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Models.DTO.Quest.Task;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestTaskReposponseController(IQuestTaskResponseService service) : ControllerBase
    {
        [HttpPost("create")]
        public async Task CreateAsync(CreateQuestTaskResponseDTO dto) => await service.AnswerAsync(dto);
    }
}

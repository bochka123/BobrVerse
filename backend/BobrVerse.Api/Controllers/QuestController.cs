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
        public async Task<ApiResponse<AuthorQuestDTO>> Create(CreateQuestDTO dto) => new ApiResponse<AuthorQuestDTO>(await service.CreateAsync(dto));
    }
}

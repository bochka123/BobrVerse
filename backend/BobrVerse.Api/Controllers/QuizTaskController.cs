using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.Quest.Task;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/[controller]")]
    [MainAuth]
    [ApiController]
    public class QuizTaskController(IQuizTaskService service) : ControllerBase
    {
        [HttpPost]
        public async Task<ApiResponse> Create(CreateTaskDTO dto)
        {
            await service.CreateAsync(dto);
            return new ApiResponse(true);
        }
    }
}

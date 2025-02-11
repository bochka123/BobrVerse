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
        public async Task<ApiResponse<QuizTaskDTO>> Create(CreateTaskDTO dto) => new ApiResponse<QuizTaskDTO>(await service.CreateAsync(dto));

        [HttpPut]
        public async Task<ApiResponse<QuizTaskDTO>> Update(QuizTaskDTO dto) => new ApiResponse<QuizTaskDTO>(await service.UpdateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            await service.DeleteAsync(id);
            return new ApiResponse(true);
        }
    }
}

using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Bll.Services;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.File;
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

        [HttpGet("getQuestTask/{questId:guid}/{order:int}")]
        public async Task<ApiResponse<QuizTaskDTO>> GetByOrder(Guid questId, int order) 
            => new ApiResponse<QuizTaskDTO>(await service.GetByOrderAsync(questId, order) ?? throw new BobrException($"Task related to quest with id {questId} and with order {order} not found."));

        [HttpPut("uploadPhoto/{questTaskId:guid}")]
        public async Task<ApiResponse<FileDto>> UploadPhoto([FromRoute] Guid questTaskId) => new ApiResponse<FileDto>(await service.UploadPhotoAsync(await Request.ReadFormAsync(), questTaskId));

        [HttpDelete("deletePhoto/{questTaskId:guid}")]
        public async Task<ApiResponse> DeletePhoto([FromRoute] Guid questTaskId) => new ApiResponse(await service.DeletePhotoAsync(questTaskId));
    }
}

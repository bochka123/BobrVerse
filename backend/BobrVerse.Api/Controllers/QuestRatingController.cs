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
    }
}
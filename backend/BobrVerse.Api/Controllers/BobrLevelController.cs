using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.BobrLevel;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MainAuth]
    public class BobrLevelController(IBobrLevelService service)
    {
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<BobrLevelDTO>>> GetAll() => new ApiResponse<IEnumerable<BobrLevelDTO>>(await service.GetAll());
    }
}

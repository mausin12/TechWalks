using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string? filterOn, string? filterQuery)
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechWalks.API.Models.Dto;

namespace TechWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser, dto.Password);

            if (identityResult.Succeeded)
            {
                if (dto.Roles != null && dto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, dto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered successfully");
                    }
                }
            }
            return BadRequest("Something went wrong...");
        }
    }
}

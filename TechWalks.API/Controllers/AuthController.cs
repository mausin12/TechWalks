using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechWalks.API.Models.Dto;
using TechWalks.API.Repositories;

namespace TechWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Username);
            if (user != null)
            {
                bool isPwdValid = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (isPwdValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username and/or Password is wrong");
        }
    }
}

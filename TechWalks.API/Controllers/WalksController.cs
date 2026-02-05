using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWalks.API.Models.Domain;
using TechWalks.API.Models.Dto;
using TechWalks.API.Repositories;

namespace TechWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto dto)
        {
            var walk = _mapper.Map<Walk>(dto);
            walk = await _walkRepository.CreateAsync(walk);
            return Ok(_mapper.Map<WalkDto>(walk));
        }
    }
}

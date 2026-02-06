using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWalks.API.CustomActionFilters;
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
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto dto)
        {

            var walk = _mapper.Map<Walk>(dto);
            walk = await _walkRepository.CreateAsync(walk);
            return Ok(_mapper.Map<WalkDto>(walk));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterTerm,
                                                 [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                                                 [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 50)
        {
            var walks = await _walkRepository.GetAllAsync(filterOn, filterTerm, 
                                                            sortBy, isAscending ?? true,
                                                            pageNo, pageSize);
            return Ok(_mapper.Map<List<WalkDto>>(walks));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await _walkRepository.GetByIdAsync(id);
            if (walk == null) { return NotFound(); }
            return Ok(_mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, UpdateWalkDto dto)
        {

            var walk = _mapper.Map<Walk>(dto);
            walk = await _walkRepository.UpdateAsync(id, walk);
            if (walk == null) return NotFound();
            return Ok(_mapper.Map<WalkDto>(walk));

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walk = await _walkRepository.DeleteAsync(id);
            if (walk == null) return NotFound();
            return Ok(_mapper.Map<WalkDto>(walk));
        }
    }
}

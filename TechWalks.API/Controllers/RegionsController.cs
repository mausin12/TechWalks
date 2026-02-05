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
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _regionRepository.GetAllAsync();
            var regionDtos = _mapper.Map<List<RegionDto>>(regions);
            return Ok(regionDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            var region = _mapper.Map<Region>(createRegionDto);

            region = await _regionRepository.CreateAsync(region);

            var regionDto = _mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { Id = region.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var region = _mapper.Map<Region>(updateRegionDto);

            region = await _regionRepository.UpdateAsync(id, region);
            if (region == null)
                return NotFound();

            //return NoContent(); return 204 No Content Response

            return Ok(_mapper.Map<RegionDto>(region));
        }
    }
}

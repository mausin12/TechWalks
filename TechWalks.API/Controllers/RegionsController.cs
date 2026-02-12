using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles ="Reader")] //Commented to avoid log in for logging checking
        public async Task<IActionResult> GetAll()
        {
                throw new Exception("Custom Exception");
                var regions = await _regionRepository.GetAllAsync();
                var regionDtos = _mapper.Map<List<RegionDto>>(regions);
                return Ok(regionDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
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
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {

            var region = _mapper.Map<Region>(createRegionDto);

            region = await _regionRepository.CreateAsync(region);

            var regionDto = _mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { Id = region.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {

            var region = _mapper.Map<Region>(updateRegionDto);

            region = await _regionRepository.UpdateAsync(id, region);
            if (region == null)
                return NotFound();

            //return NoContent(); return 204 No Content Response

            return Ok(_mapper.Map<RegionDto>(region));

        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);

            if (region == null)
                return NotFound();

            //return NoContent();

            // Returning 200 OK with a message body
            return Ok(new
            {
                message = "Region deleted successfully.",
                deletedId = id,
                timestamp = DateTime.UtcNow
            });
        }
    }
}

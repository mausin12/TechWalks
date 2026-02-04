using AutoMapper;
using TechWalks.API.Models.Domain;
using TechWalks.API.Models.Dto;

namespace TechWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}

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
            CreateMap<Region, CreateRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();
            CreateMap<Walk, CreateWalkDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
           
            CreateMap<Walk, UpdateWalkDto>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) //Use this if Property names are different
                .ReverseMap();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TechWalks.API.Models.Dto
{
    public class UpdateRegionDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

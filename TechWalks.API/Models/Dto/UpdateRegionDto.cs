using System.ComponentModel.DataAnnotations;

namespace TechWalks.API.Models.Dto
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "The Code should be 3 characters long")]
        [MaxLength(3, ErrorMessage = "The Code should be 3 characters long")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

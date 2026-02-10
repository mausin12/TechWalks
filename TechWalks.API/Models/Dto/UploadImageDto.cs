using System.ComponentModel.DataAnnotations;

namespace TechWalks.API.Models.Dto
{
    public class UploadImageDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
}

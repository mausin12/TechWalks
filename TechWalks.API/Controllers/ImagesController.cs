using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWalks.API.Models.Domain;
using TechWalks.API.Models.Dto;
using TechWalks.API.Repositories;

namespace TechWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageDto dto)
        {
            ValidateFileUpload(dto);
            if(ModelState.IsValid)
            {
                var image = new Image
                {
                    File = dto.File,
                    FileExtension = Path.GetExtension(dto.File.FileName),
                    FileSizeInBytes = dto.File.Length,
                    FileName = dto.FileName,
                    FileDescription = dto.FileDescription,
                };

                await _imageRepository.Upload(image);
                return Ok(image);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadImageDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()))
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File should be 10MB or less");
            }
        }
    }
}

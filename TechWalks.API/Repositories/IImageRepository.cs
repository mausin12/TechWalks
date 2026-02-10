using TechWalks.API.Models.Domain;

namespace TechWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

using TechWalks.API.Models.Domain;

namespace TechWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}

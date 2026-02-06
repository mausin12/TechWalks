using TechWalks.API.Models.Domain;

namespace TechWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync(string? filterOn, string? filterTerm,
                                    string? sortBy, bool isAscending = true,
                                    int pageNo = 1, int pageSize = 50);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}

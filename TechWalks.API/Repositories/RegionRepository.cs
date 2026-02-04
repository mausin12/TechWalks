using Microsoft.EntityFrameworkCore;
using TechWalks.API.Data;
using TechWalks.API.Models.Domain;

namespace TechWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext _dbContext;

        public RegionRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }
    }
}

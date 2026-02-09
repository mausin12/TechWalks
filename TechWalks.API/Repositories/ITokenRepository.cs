using Microsoft.AspNetCore.Identity;

namespace TechWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}

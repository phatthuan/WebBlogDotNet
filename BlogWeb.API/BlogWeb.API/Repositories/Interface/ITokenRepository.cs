using Microsoft.AspNetCore.Identity;

namespace BlogWeb.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}

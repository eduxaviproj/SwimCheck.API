using Microsoft.AspNetCore.Identity;

namespace SwimCheck.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles); //return type as string because of JWT Token being a string
    }
}

using System;
namespace AuthenticationService.Data
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

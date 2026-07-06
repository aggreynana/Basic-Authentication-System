using System.Security.Claims;
using BasicAuth.Model.UserDto;

namespace BasicAuth.Extensions;

public static class PrincipalExtention
{
    public static AuthData GetAuthData(this ClaimsPrincipal principal)
    {
        var id = principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var token = principal.FindFirstValue(ClaimTypes.Thumbprint) ?? string.Empty;

        return new AuthData()
        {
            Id = id,
            Token = token
        };
    }
}
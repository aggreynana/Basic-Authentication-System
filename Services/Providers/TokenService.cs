using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BasicAuth.Entities;
using BasicAuth.Model;
using BasicAuth.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BasicAuth.Services.Providers;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSettingsOptions)
    {
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public async Task<string> GenerateToken(UserEntity user)
    {
        await Task.Delay(0);
        
        // prepare user claims
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new("FirstName", user.FirstName),
            new("LastName", user.LastName),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        // Get the key
        // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var key = new SymmetricSecurityKey(keyBytes);


        // Signing the claims
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        // Formulate the token
        var token = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_jwtSettings.DurationInSeconds),
            signingCredentials: credentials
        );


        // Generate the token string
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
}
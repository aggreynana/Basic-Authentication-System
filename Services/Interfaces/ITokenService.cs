using BasicAuth.Entities;

namespace BasicAuth.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateToken(UserEntity user);
}
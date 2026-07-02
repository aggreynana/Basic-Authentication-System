using BasicAuth.Entities;
using BasicAuth.Model.UserDto;
using BasicAuth.Models;

namespace BasicAuth.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<AuthResponseDto>> CreateUserAsync(CreateUserRequestDto user);

    // Task<ApiResponse<AuthResponseDto>> 
}